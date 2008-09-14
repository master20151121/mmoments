using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Media;
using System.IO;

namespace helloword
{
    class Program
    {
        [DllImport("FooID.dll")]
        public static extern void fp_init(int samplerate, int channels);
        [DllImport("FooID.dll")]
        public static extern void fp_free();
        [DllImport("FooID.dll")]
        public static extern int fp_feed_short(short[,] data, int size);
        [DllImport("FooID.dll")]
        public static extern int fp_feed_float(short[,] data, int size);
        [DllImport("FooID.dll")]
        public static extern int fp_getsize();
        [DllImport("FooID.dll")]
        public static extern int fp_getversion();
        [DllImport("FooID.dll")]
        public static extern int fp_calculate(int songlen, char[] buff);// unsigned char* buff ?
        

        // public static char[] print; // trying to help access error.

        static void Main(string[] args)
        {
            fp_init(721920, 1); // 705 kbps momo, 1 channel.
            
            string url = "song.wav";
            System.IO.FileStream fs = new System.IO.FileStream(@url, FileMode.Open, FileAccess.Read);
            byte[] barray = new byte[fs.Length - 44]; // length without header.

            fs.Read(barray, 0, barray.Length);

            short[,] s2 = new short[barray.Length, 1];

            // a short is a byte

            for (int i = 0; i < barray.Length; i++)
            {
                s2[i,0] = barray[i];
            }

            for (int i = 0; i < barray.Length; i++)
            {
                int ans = fp_feed_float(s2, barray.Length);
                if (ans > 1)
                    break;
            }

            Console.WriteLine("finished feeding");
            
            char[] print = new char[fp_getsize()];
            Console.WriteLine("start calculate");

            int fprint = fp_calculate(300, print); // Access violation error, protected access. 

            Console.WriteLine(fprint.ToString());
            Console.WriteLine("end");

            System.Console.Read();
        }
    }
}
