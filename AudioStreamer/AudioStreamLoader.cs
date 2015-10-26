using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;
using NAudio.Wave;

namespace AudioPlayer
{
     class AudioStreamLoader
    {
        TcpClient tcpClient;
        AudioStream audioStream;
        public AudioStreamLoader(AudioStream audioStream)
        {
            this.audioStream = audioStream;
            tcpClient = new TcpClient("192.168.101.124", 8885);
            Console.WriteLine("Connected.");
        }
        public void streamAudio()
        {
           
            byte[] buffer = new byte[65536];
            int read;
            while ((read = tcpClient.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                byte[] finalBuffer = new byte[read];
                Array.Copy(buffer, finalBuffer, read);
                audioStream.Load(finalBuffer);
                
            }
        }
    }
}
