#define DEBUG
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

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
            matches= new SongList();
        }

        public void record()
        {
            DynamicRecord.start();
        }
        public void stoprecord()
        {
            recordedwav= DynamicRecord.stop();
            fingerprint = calcfinger.generate(recordedwav);
            recordtime = DateTime.Now;
        }
        public bool askserver() // return true on succeed, return false on fail.
        {
            //sorry dont realy know what needs to be done here.
            return false;
        }

        public string getwavurl() { return recordedwav; }
        public DateTime getrecordtime() { return recordtime; }
        public string getfingerprint() { return fingerprint; }
        public SongList getsonglist() { return matches; }
        public void setwaveurl(string url) { recordedwav = url; }
        public void setdatetime(string dt) { recordtime = Convert.ToDateTime(dt); }//untested
        public void setfingerprint(string print) { fingerprint = print; }
        public void setmatches(SongList sl) { matches = sl; }
    }

    //somewhere we should have a list of these songinstances.
    //here is something to save/load them.

    class csvmanager
    {
        static string SAVEFILE = "instancesaver.txt";

        public static void save(List<songinstance> list)
        {
            string fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            fileName = Path.GetDirectoryName(fileName);
            fileName = Path.Combine(fileName, SAVEFILE);
            TextWriter tw;
            System.Windows.Forms.MessageBox.Show("csvmanager, saving");
            try
            {
                tw = new StreamWriter(fileName, false);//overwrite.                
                foreach (songinstance si in list)
                {
                    string line = si.getwavurl() + "," + si.getrecordtime().ToString() + "," + si.getfingerprint() + ",";
                    SongList silist = si.getsonglist();
                    for (int i = 0; i < silist.Count; i++)
                    {
                        line += silist[i].artist + "," + silist[i].title + ",";
                    }
                    tw.WriteLine(line);
                }
                tw.Close();
            }
            catch (IOException)
            {
                System.Windows.Forms.MessageBox.Show("IOException, out of diskspace?");
            }
        }

        public static List<songinstance> load()
        {
            System.Windows.Forms.MessageBox.Show("csvmanager loading");
            string fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            fileName = Path.GetDirectoryName(fileName);
            fileName = Path.Combine(fileName, SAVEFILE);
            List<songinstance> returnlist = new List<songinstance>();
            try
            {
                TextReader tr = new StreamReader(fileName);
                string line;
                string value;
                while ((line = tr.ReadLine()) != null)
                {
                    songinstance si = new songinstance();

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(",") + 1);
                    si.setwaveurl(value);
#if DEBUG
                    System.Windows.Forms.MessageBox.Show("value :"+value);
#endif

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(",") + 1);
                    si.setdatetime(value);
#if DEBUG
                    System.Windows.Forms.MessageBox.Show("value :" + value);
#endif

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(",") + 1);
                    si.setfingerprint(value);
#if DEBUG
                    System.Windows.Forms.MessageBox.Show("value :" + value);
#endif

                    SongList sl = new SongList();
                    int cindex;
                    while ((cindex = line.IndexOf(",")) != -1) //-1?
                    {
                        Song s = new Song();
                        value = line.Substring(0, line.IndexOf(","));
                        line = line.Substring(line.IndexOf(",")+1);
                        s.artist = value;
                        value = line.Substring(0, line.IndexOf(","));
                        line = line.Substring(line.IndexOf(",") + 1);
                        s.title = value;
                        value = line.Substring(0, line.IndexOf(","));
                        line = line.Substring(line.IndexOf(",") + 1);
                        s.match = Int32.Parse(value);
                        sl.Add(s);
                    }
                    returnlist.Add(si);

                }
                return returnlist;
            }
            catch (FileNotFoundException) { return returnlist; }//returnlist is empty, that should be fine.
            catch (IOException)
            {
                System.Windows.Forms.MessageBox.Show("IOException, this is odd.");
                return returnlist;
            }
        }
    }


    //csv format
    // recordurl,datetime,fingerprint,songlistitem1.artest,songlistitem1.title,songlistitem1.match,songlistitem2.artest,songlistitem2.title,etc

}