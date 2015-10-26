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
   
    class AudioStream
    {

        
        public string loadProgress;
        private WaveOut waveOut;
        public Stream ms;
        public WaveStream blockAlignedStream;
        public AudioStream()      
        {
            ms = new MemoryStream();
            waveOut = null;
            blockAlignedStream = null;                    
        }

        public void Load(byte[] buffer)
        {           
            var pos = ms.Position;
            ms.Position = ms.Length;
            ms.Write(buffer, 0, buffer.Length);
            ms.Position = pos;
            //Thread.Sleep(2000); 
            if (blockAlignedStream == null)
            {               
                initializeStream();
            }            
            loadProgress = Convert.ToString(ms.Length);
            Console.WriteLine(loadProgress);
        }

        public void pause()
        {
            waveOut.Pause();
            Console.WriteLine("paused");
        }
        
        public void initializeStream()
        {
            if (ms.Length > 65536)
            {
                ms.Position = 0;

                blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms)));
                blockAlignedStream.Position = 0;

                waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
                waveOut.Init(blockAlignedStream);               
            }
        }

        public void play()
        {
            if (ms.Position < ms.Length - 65536)
            {
                waveOut.Play();
                Console.WriteLine("playing");
            }
        }
    }
}
