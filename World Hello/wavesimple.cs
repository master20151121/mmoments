using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//code double ups are cool huh.
namespace mmoments
{
    class wavesimple
    {
    static byte[] RIFF;
        static byte[] FMT;
        static byte[] DATA_HEADER;
        static byte[] DATA;
        //static ushort[] DATA;
        
        public wavesimple(string file_url)
        {            
            try
            {
                System.IO.FileInfo fi = new FileInfo(file_url);
                long filesize = fi.Length;

                //Console.WriteLine("file size {0}", filesize.ToString());
                
                FileStream fs = fi.Open(FileMode.Open);
                
                BinaryReader br = new BinaryReader(fs);

                RIFF = br.ReadBytes(12);

                FMT = br.ReadBytes(24);
                
                DATA_HEADER = br.ReadBytes(8);
                
                DATA = br.ReadBytes(Convert.ToInt32(filesize - 44));

                //Console.WriteLine("file possition {0}", fs.Position.ToString());

                Console.WriteLine("loaded wav");

                if (fs.Position != Convert.ToInt32(filesize)) // simple test. shouldnt ever happen, unless my math suddenly breaks.
                {
                    Console.WriteLine(" fs.position != filesize !!!");
                    //Environment.Exit(1); //strange cant be done in this project.                
                }

                fs.Close();                
                
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found : {0}", file_url);
                //Environment.Exit(1);
            }
        }

        //public int get_frame_size()
        //{


        //    return 0;

        //}

        public int get_no_channels()
        {
            int c = Convert.ToInt32(FMT[10]);
            return c;
        }

        public uint get_frame_rate()
        { // little edian. Backwards addressing. // 000044100 vs 441000000?? // Uint.
            uint fr = Convert.ToUInt32(FMT[15]);            
            fr = fr << 24;
            uint fa = Convert.ToUInt32(FMT[14]);
            fa = fa << 16;
            fr = fr + fa;
            fa = Convert.ToUInt32(FMT[13]);
            fa = fa << 8;
            fr = fr + fa;
            fa = Convert.ToUInt32(FMT[12]);
            fr = fr + fa;
            return fr; // 44100
        }
                
        public uint get_bits_pr_sample()
        { // little edian. // should be ushort, but shift opp, cbf.
            uint bps = Convert.ToUInt32(FMT[23]);
            bps = bps << 8;
            bps = bps + Convert.ToUInt32(FMT[22]);            
            return bps;
        }

        static int read_count = 0;

        public int read(ref byte[] ba)
        {
            try
            {
                for (int i = 0; i < ba.Length; i++)
                {
                    ba[i] = DATA[i + read_count];
                }
            }
            catch (IndexOutOfRangeException)
            { return 0; } //endoffile.
            read_count += ba.Length;
            return ba.Length;
        }
    }
}
