using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace AudioPlayer
{
    class TCPClient
    {
        public void Client()
        {

            {

                TcpClient tcpClient = new TcpClient("192.168.101.124", 8888);
                Console.WriteLine("Connected.");
                //MediaPlayer player = new MediaPlayer();
                StreamReader reader = new StreamReader(tcpClient.GetStream());

                string cmdFileSize = reader.ReadLine();
                string cmdFileName = reader.ReadLine();

                int length = Convert.ToInt32(cmdFileSize);
                byte[] buffer = new byte[length];
                int received = 0;
                int read = 0;
                int size = 1024;
                int remaining = 0;

                while (received < length)
                {
                    remaining = length - received;
                    if (remaining < size)
                    {
                        size = remaining;
                    }

                    read = tcpClient.GetStream().Read(buffer, received, size);
                    received += read;
                }
                using (FileStream fStream = new FileStream(Path.GetFileName(cmdFileName), FileMode.Create))
                {                  
                    fStream.Write(buffer, 0, buffer.Length);
                    fStream.Flush();
                    fStream.Close();               
                }
                
                Console.WriteLine("File received");
               // player.Show();
              //  player.PlayStream(cmdFileName);
            }
        }
    }
}

