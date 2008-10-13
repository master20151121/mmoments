using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace World_Hello
{
    class Recorder
    {
        public static String Record()
        {
	    WaveIn wi = null;

            try
            {
                wi = new WaveIn();

                uint numDevices = wi.NumDevices();
                if (numDevices < 1)
                {
                    //ERROR: No valid sound drivers detected
                }

                if (Wave.MMSYSERR.NOERROR != wi.Preload(30000, 256 * 1024))
                {
                    //ERROR: Failed to preload buffers
                }

                if (Wave.MMSYSERR.NOERROR != wi.Start())
                {
                    //ERROR: Failed to start recording
                }

                
                Thread.Sleep(30000);

                wi.Stop();

                String fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                fileName = Path.GetDirectoryName(fileName);
                fileName = Path.Combine(fileName, "recording.wav");
                if (Wave.MMSYSERR.NOERROR != wi.Save(fileName))
                {
                    //ERROR: FAILED TO SAVE
                }
                wi.Dispose();

                return fileName;
            }
            catch (Exception e)
            {
                //failed to record
                return "ERROR " + e.Message;
            }
        }
    }
}
