using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;
using NAudio.Wave;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for ClientPlayer.xaml
    /// </summary>
    public partial class ClientPlayer : Window
    {

        MediaPlayer mediaPlayer = new MediaPlayer();
        private Stream ms = new MemoryStream();
        public void PlayMp3FromServer()
        {
            new Thread(delegate(object o)
            {
                TcpClient tcpClient = new TcpClient("192.168.101.124", 8885);
                Console.WriteLine("Connected.");
                StreamReader reader = new StreamReader(tcpClient.GetStream());

                byte[] buffer = new byte[65536];
                int read;
                while ((read = tcpClient.GetStream().Read(buffer, 0, buffer.Length)) > 0)
                {
                    var pos = ms.Position;
                    ms.Position = ms.Length;
                    ms.Write(buffer, 0, read);
                    ms.Position = pos;
                    Thread.Sleep(2000);
                    Console.WriteLine(Convert.ToString(ms.Length));

                }
            }).Start();
            while (ms.Length < 65536)
                Thread.Sleep(1000);
           
            ms.Position = 0;
            using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms))))
            {
                using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    ClientPlayer player = new ClientPlayer();
                    waveOut.Init(blockAlignedStream);
                   // mediaPlayer.Show();
                    
                   // EventHandler<Button_Click>(_waveOutDevice_PlaybackStopped);</stoppedeventargs>
                    while (true)
                    {

                        if (ms.Position < ms.Length - 65536)
                        {                          
                            waveOut.Play();
                            Console.WriteLine("playing");
                        }
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
            {
                waveOut.Pause();
            }

        }
    }
}



