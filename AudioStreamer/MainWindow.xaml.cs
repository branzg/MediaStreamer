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
        public Program()
        {
            InitializeComponent();         
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread newClientThread = new Thread(new ThreadStart(() =>
                {
                   ClientPlayer clientPlayer = new ClientPlayer();
                   // clientPlayer.Show();
                    //clientPlayer.PlayMp3FromServer();
                    TCPClient client = new TCPClient();
                    client.PlayMp3FromServer();
                    System.Windows.Threading.Dispatcher.Run();
                }));
        newClientThread.SetApartmentState(ApartmentState.STA);
        newClientThread.IsBackground = true;
        newClientThread.Start();
            
        }
    }          
}