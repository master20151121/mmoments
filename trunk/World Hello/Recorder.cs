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
           int RTime = 30000;

            try
            {
                wi = new WaveIn();

                uint numDevices = wi.NumDevices();
                if (numDevices < 1)
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: No valid sound drivers detected");
                    //ERROR: No valid sound drivers detected
                }

                if (Wave.MMSYSERR.NOERROR != wi.Preload(RTime, 256 * 1024))
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: Failed to preload buffers");
                    //ERROR: Failed to preload buffers
                }

                if (Wave.MMSYSERR.NOERROR != wi.Start())
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: Failed to start recording");
                    //ERROR: Failed to start recording
                }

                
                Thread.Sleep(RTime);

                wi.Stop();

                String fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                fileName = Path.GetDirectoryName(fileName);
                fileName = Path.Combine(fileName, "recording.wav");
                if (Wave.MMSYSERR.NOERROR != wi.Save(fileName))
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: FAILED TO SAVE");
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

    class DynamicRecord
    {
        static WaveIn wi;
        static int UPPERLIMMIT = 500000; // 5mins, bad things might happen if the user doesnt stop recording by this time. It'll be around 50mb!
        static bool running;

        public static void start()
        {
            if (running)
            {
                System.Windows.Forms.MessageBox.Show("already recording");
                return;
            }

            running = true;
            try
            {
                wi = new WaveIn();

                uint numDevices = wi.NumDevices();
                if (numDevices < 1)
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: No valid sound drivers detected");
                    //ERROR: No valid sound drivers detected
                }

                if (Wave.MMSYSERR.NOERROR != wi.Preload(UPPERLIMMIT, 256 * 1024))
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: Failed to preload buffers");
                    //ERROR: Failed to preload buffers
                }

                if (Wave.MMSYSERR.NOERROR != wi.Start())
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: Failed to start recording");
                    //ERROR: Failed to start recording
                }
            }
            catch (Exception e)
            {
                //failed to record
                System.Windows.Forms.MessageBox.Show("Error " + e.Message);
            }
        }

        public static string stop() //returns filename.
        {
            try
            {
                wi.Stop();

                string fileName = genfilename();
                
                if (Wave.MMSYSERR.NOERROR != wi.Save(fileName))
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: FAILED TO SAVE");
                    //ERROR: FAILED TO SAVE
                    //I think when this happens it leaves a file with size in the order of the UPPERLIMMIT.
                }
                wi.Dispose();

                running = false;
                return fileName;
            }
            catch (Exception e)
            {
                //failed to record
                System.Windows.Forms.MessageBox.Show("Error " + e.Message);
                return "ERROR " + e.Message;
            }
        }


        static int filecount = 1;
        static string genfilename()
        {
            string fileName;
            string n;
            while (true) //should only loop once unless my math breaks.
            {
                fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                fileName = Path.GetDirectoryName(fileName);
                n = "recording" + filecount.ToString() + ".wav";
                fileName = Path.Combine(fileName, n);
                FileInfo fi = new FileInfo(fileName);
                if (fi.Exists == false)
                    break;
                filecount++;
            }
            return fileName;
        }

    }
}
