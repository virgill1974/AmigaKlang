using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;




namespace AmigaKlangGUI
{

    
    class Class1
    {



/*
        public static void PlayBeep(byte[,] buffer, int number, int samples)
    {
          MemoryStream mStrm = new MemoryStream();
          BinaryWriter writer = new BinaryWriter(mStrm);

        int     formatChunkSize = 16;
        int     headerSize = 8;
        short   formatType = 1;
        short   tracks = 1;
        int     samplesPerSecond = 11000;
        short   bitsPerSample = 16;
        short   frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
        int     bytesPerSecond = samplesPerSecond * frameSize;
        int     waveSize = 4;
        //int samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
        int     dataChunkSize = samples * frameSize;
        int     fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
        // var encoding = new System.Text.UTF8Encoding();
        writer.Write(0x46464952); // = encoding.GetBytes("RIFF")
        writer.Write(fileSize);
        writer.Write(0x45564157); // = encoding.GetBytes("WAVE")
        writer.Write(0x20746D66); // = encoding.GetBytes("fmt ")
        writer.Write(formatChunkSize);
        writer.Write(formatType);
        writer.Write(tracks);
        writer.Write(samplesPerSecond);
        writer.Write(bytesPerSecond);
        writer.Write(frameSize);
        writer.Write(bitsPerSample);
        writer.Write(0x61746164); // = encoding.GetBytes("data")
        writer.Write(dataChunkSize);
        {
            for (int smp = 0; smp < samples; smp++)
            {
                    writer.Write(buffer[smp,number]);
            }
        }
          



            mStrm.Seek(0, SeekOrigin.Begin);
        new System.Media.SoundPlayer(mStrm).Play();
        
        mStrm.Close();
        writer.Close();
        } // public static void PlayBeep(UInt16 frequency, int msDuration, UInt16 volume = 16383)




    */





        private WaveOutEvent outputDevice;
        //private AudioFileReader audioFile;

        public static void PlayPCM(byte[] pcm, int length, int sampleRate)
        {
            byte[] doubled = null;

            using (var writer = new BinaryWriter(new MemoryStream()))
            {
                using (var reader = new BinaryReader(new MemoryStream(pcm)))
                {
                    while (reader.BaseStream.Position < length)
                    {
                        writer.Write((byte)0x00);
                        writer.Write(reader.ReadByte());
                    }
                }
                doubled = ((MemoryStream)writer.BaseStream).ToArray();
            }

            //var sampleRate = 22000;
            var ms = new MemoryStream(doubled);

            var rs = new RawSourceWaveStream(ms, new WaveFormat(sampleRate, 16, 1));
            Form1.waveout.Stop();
            Form1.waveout.Volume = 0.3f;
            Form1.waveout.Init(rs);
            Form1.waveout.Play();
                
               // while (Form1.waveout.PlaybackState == PlaybackState.Playing)
                 //      {
              //  if (ms.Position == ms.Length) ms.Position = 60000;
                   //     }
            //            wo.Dispose();
        }

        private object RawSourceWaveStream(MemoryStream ms)
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            //if (audioFile == null)
            //{
            //  audioFile = new AudioFileReader(@"C:\tmp\test.wav");
            //  outputDevice.Init(audioFile);
            //}
            outputDevice.DesiredLatency = 100;
            outputDevice.Play();
            
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
             outputDevice.Dispose();
            outputDevice = null;
            //audioFile.Dispose();
            //audioFile = null;
        }

    }
}
