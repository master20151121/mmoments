using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace winclient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("welcome to the windows client for mmoments");

            Console.WriteLine("enter to start recording");
            Console.ReadLine();

            Recorder.Start();
            Console.WriteLine("starting recording");
            Console.WriteLine("enter to stop recording");
            Console.ReadLine();

            string file = Recorder.Stop();

            string fingerprint = calcfinger.generate(file);

            Connector c = new Connector();
            Console.WriteLine("enter the ip to connect to, eg \"127.0.0.0\"");
            String ip = Console.ReadLine();
            c.Connect(ip);
            SongList sl = c.SendFingerprint(fingerprint);

            foreach (Song s in sl.Songs)
            {
                Console.WriteLine("{0}, {1}, {2}", s.title, s.artist, s.match);
            }
        }
    }
}