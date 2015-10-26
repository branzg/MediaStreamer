using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading;
using Microsoft.Win32;
using NAudio.Wave;



namespace AudioPlayer
{
    public partial class Program : Window
    {
        AudioStream audioStream;
        AudioStreamLoader audioStreamLoader;
        public Program()
        {
            InitializeComponent();          
        }

        private void Server_Click(object sender, RoutedEventArgs e)
        {
            Thread newServerThread = new Thread(new ThreadStart(() =>
            {      
                TCPServer server = new TCPServer();
                server.Server();
                System.Windows.Threading.Dispatcher.Run();
            }));
            newServerThread.SetApartmentState(ApartmentState.STA);
            newServerThread.IsBackground = true;
            newServerThread.Start();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Thread newClientThread = new Thread(new ThreadStart(() =>
            {
                audioStream = new AudioStream();
                audioStreamLoader = new AudioStreamLoader(audioStream);
                new Thread(delegate()
                {
                    audioStreamLoader.streamAudio();
                }).Start();
                System.Windows.Threading.Dispatcher.Run();
            }));
            newClientThread.SetApartmentState(ApartmentState.STA);
            newClientThread.IsBackground = true;
            newClientThread.Start();         
        }

        private void pause_buttton_click(object sender, RoutedEventArgs e)
        {
            audioStream.pause();
        }

        private void play_button_click(object sender, RoutedEventArgs e)
        {
            audioStream.play();           
        }
    }          
}