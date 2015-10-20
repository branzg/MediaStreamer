using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace AudioPlayer
{
    public partial class Program : Window
    {


        public Program()
        {
            InitializeComponent();

            
           // MediaPlayer mediaPlayer = new MediaPlayer();
           // mediaPlayer.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TCPServer server = new TCPServer();
            server.Server();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TCPClient client = new TCPClient();
            client.Client();
        }
    }       
    
}