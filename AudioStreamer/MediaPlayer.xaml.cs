using System;

using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace AudioPlayer
{
    public partial class MediaPlayer : Window
    {
        private bool mediaPlayerIsPlaying;
        private bool userIsDraggingSlider;

        public MediaPlayer()
        {
            InitializeComponent();           
            mediaPlayerIsPlaying = false;
            userIsDraggingSlider = false;
            
            //BFT TODO: Make this variable a member variable
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }  
        private void timer_Tick(object sender, EventArgs e)
        {
            //BFT TODO: Name mePlayer something more descriptive
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            { 
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mediaPlayer.Position.TotalSeconds;
            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.wmv,*.mp3;*.mpg;*.mpeg)|*.wmv;*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                mediaPlayer.Source = new Uri(openFileDialog.FileName);
        }
        //public void PlayStream(fileName)
        //{
           
        //    //mediaPlayer.Source = new Uri(fileName);
        //    mediaPlayer.Play();
        //    mediaPlayerIsPlaying = true;
        //}

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mediaPlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }
    }
}