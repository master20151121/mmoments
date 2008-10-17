using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using WaveLib;

namespace winclient
{
    class Recorder
    {
        static private WaveLib.WaveInRecorder m_Recorder;
        static private MemoryStream RecorderOutputStream = null;
        static private FileStream fs = null;
        static private byte[] m_RecBuffer;

        static private void DataArrived(IntPtr data, int size)
        {
            if (m_RecBuffer == null || m_RecBuffer.Length < size)
                m_RecBuffer = new byte[size];
            System.Runtime.InteropServices.Marshal.Copy(data, m_RecBuffer, 0, size);
            RecorderOutputStream.Write(m_RecBuffer, 0, m_RecBuffer.Length);
        }

        static public string Stop()
        {
            WaveLib.WaveFormat m_Format = new WaveLib.WaveFormat(44100, 16, 2);
            if (m_Recorder != null)
            {
                try
                {
                    // chunksize is length of wave data and the header.
                    long chunksize = RecorderOutputStream.Length + 36;

                    // writing wave header and data
                    System.IO.BinaryWriter bw = new BinaryWriter(fs);

                    WriteChars(bw, "RIFF");
                    bw.Write((int)chunksize);
                    WriteChars(bw, "WAVEfmt ");
                    bw.Write((int)16);
                    bw.Write(m_Format.wFormatTag);
                    bw.Write(m_Format.nChannels);
                    bw.Write(m_Format.nSamplesPerSec);
                    bw.Write(m_Format.nAvgBytesPerSec);
                    bw.Write(m_Format.nBlockAlign);
                    bw.Write(m_Format.wBitsPerSample);
                    WriteChars(bw, "data");
                    bw.Write(RecorderOutputStream.Length);
                    bw.Write(RecorderOutputStream.ToArray());
                    bw.Close();
                    fs.Close();
                    m_Recorder.Dispose();
                }
                finally
                {
                    m_Recorder = null;
                }
            }
            return filename_;
        }

        static private void WriteChars(BinaryWriter wrtr, string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = (char)text[i];
                wrtr.Write(c);
            }
        }

        // initialize filename iteration counter
        static int counter = 0;
        static string filename_;

        static public void Start()
        {
            Stop();
            try
            {
                String filename = "recording";
                String ending = ".wav";

                // iterate the filename
                while (File.Exists(filename + counter + ending))
                {
                    counter++;
                }

                filename_ = filename + counter.ToString() + ending;
                // create a new file and wave recorder
                fs = new FileStream(filename + counter + ending, System.IO.FileMode.Create);
                RecorderOutputStream = new MemoryStream();
                WaveLib.WaveFormat fmt = new WaveLib.WaveFormat(44100, 16, 2);
                m_Recorder = new WaveLib.WaveInRecorder(-1, fmt, 16384, 3, new WaveLib.BufferDoneEventHandler(DataArrived));

            }
            catch
            {
                Stop();
                throw;
            }
        }
    }
}
