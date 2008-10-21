using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace World_Hello
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new SplashScreen());
        }
    }

    class songinstance
    {
        string recordedwav;
        DateTime recordtime;
        string fingerprint;
        SongList matches;

        public songinstance()
        {
            recordtime = DateTime.Now;
        }

        public void record()
        {
            DynamicRecord.start();
        }
        public void stoprecord()
        {
            recordedwav= DynamicRecord.stop();
            fingerprint = calcfinger.generate(recordedwav);
        }
        public bool askserver() // return true on succeed, return false on fail.
        {
            //sorry dont realy know what needs to be done here.
            return false;
        }
    }
}