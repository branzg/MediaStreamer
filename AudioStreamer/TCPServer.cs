﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Web;

namespace AudioPlayer
{
    public class TCPServer
    {

        public void Server()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 8885);
            tcpListener.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Connected to client");

                try
                {
                    string fileName = @"C:\users\bguild_be\Desktop\Maids.mp3";
                    StreamWriter sWriter = new StreamWriter(tcpClient.GetStream());

                    byte[] bytes = File.ReadAllBytes(fileName);

                    sWriter.WriteLine(bytes.Length.ToString());
                    sWriter.Flush();

                    sWriter.WriteLine(fileName);
                    sWriter.Flush();

                    Console.WriteLine("Sending file");
                    tcpClient.Client.SendFile(fileName);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }

                Console.WriteLine("Sending Complete");
                //break;
            }
        }
    }
}

