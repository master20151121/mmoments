using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace World_Hello
{
    class waveplayer
    {
        static Thread runningplayer;
        static string FILENAME;


        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int WCE_PlaySound(string szSound, IntPtr hMod, int flags);

        private enum Flags
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */
            SND_ASYNC = 0x0001,  /* play asynchronously */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_FILENAME = 0x00020000, /* name is file name */
        }


        public static void PlaySound(string filename)
        {
            FILENAME = filename;
            runningplayer = new Thread(playthread);
            runningplayer.IsBackground = true;
            runningplayer.Start();
        }

        private static void playthread()
        {
            //System.Windows.Forms.MessageBox.Show("starting play");
            WCE_PlaySound(FILENAME, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_FILENAME));
        }

#if WANTBROKENSTUFF
        public static void stop() //doesnt work.
        {

            try
            {
                runningplayer.Abort();                
            }
            catch (ThreadAbortException)
            {
                System.Windows.Forms.MessageBox.Show("thread abort exception");
                //i expect this,
                return;
            }
        }
#endif

    }
}



 
