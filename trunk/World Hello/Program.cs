#define VERBOSE
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
            recordtime = DateTime.Now;
        }
        public void stoprecord()
        {
            recordedwav= DynamicRecord.stop();
            if (recordedwav != "fail")
                fingerprint = calcfinger.generate(recordedwav);
        }
        public bool askserver(Interface theint, string serverip) // return true on succeed, return false on fail.
        {
            Connector con = new Connector();
            if (con.Connect(serverip) == true)
            {
                matches = con.SendFingerprint(fingerprint);

                this.showmatches(theint);

                //UI_SongList uiSongList = new UI_SongList(theint);
                //uiSongList.SL = matches;
                //uiSongList.Show();
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("failed to connect");//should be replaced with statusbar.
                //how to change status bar here?
                if (this.matches.Count >0)
                    this.showmatches(theint); //do this in both cases, one button.
                return false;
            }
            
        }

        public void showmatches(Interface theint)
        {
            UI_SongList uisl = new UI_SongList(theint);
#if VERBOSE
            MessageBox.Show("num songs " + matches.Count.ToString());
#endif
            uisl.SL = this.matches;
            uisl.Show();
            
        }

        public void delete()
        {
            try
            {
                FileInfo fi = new FileInfo(recordedwav);
                fi.Delete();
            }
            catch (IOException) { MessageBox.Show("IOException deleting recording at " + this.getrecordtime().ToString()); }
            //will need to be removed from instancelist.
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
#if VERBOSE
            MessageBox.Show("csv save: list<songinscance>.count "+list.Count);
#endif
            string fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            fileName = Path.GetDirectoryName(fileName);
            fileName = Path.Combine(fileName, SAVEFILE);
            TextWriter tw;
            //System.Windows.Forms.MessageBox.Show("csvmanager, saving");
            try
            {
                tw = new StreamWriter(fileName, false);//overwrite.                
                foreach (songinstance si in list)
                {
                    string line = si.getwavurl() + "," + si.getrecordtime().ToString() + "," + si.getfingerprint() + ",";
//#if VERBOSE
//                    MessageBox.Show("csv saving: "+line);
//#endif

                    SongList silist = si.getsonglist();
                    for (int i = 0; i < silist.Count; i++)
                    {
                        line += silist[i].artist + "," + silist[i].title + ","+silist[i].match.ToString()+",";
                    }//untested.
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

//#if VERBOSE
//                    MessageBox.Show("csv loader: "+ line);
//#endif
                    songinstance si = new songinstance();

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(",") + 1);
                    si.setwaveurl(value);

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(",") + 1);
                    si.setdatetime(value);

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(",") + 1);
                    si.setfingerprint(value); //this will not handle error.

                    SongList sl = new SongList();
                    int cindex;
                    while ((cindex = line.IndexOf(",")) != -1) //-1?
                    {
//#if VERBOSE
//                        MessageBox.Show(line);
//#endif                       
                        
                        Song s = new Song();
                        value = line.Substring(0, line.IndexOf(","));
                        line = line.Substring(line.IndexOf(",")+1);
                        s.artist = value;
                        value = line.Substring(0, line.IndexOf(","));
                        line = line.Substring(line.IndexOf(",") + 1);
                        s.title = value;
                        value = line.Substring(0, line.IndexOf(","));
//#if VERBOSE
//                        MessageBox.Show(line);
//#endif
                        line = line.Substring(line.IndexOf(",") + 1);
                        s.match = Int32.Parse(value);
//#if VERBOSE
//                        MessageBox.Show("csv adding match");
//#endif

                        //sl.Add(s); //add doesnt work.
                        sl.Insert(0, s);
                    } //this will not handle error. and is untested.
                    si.setmatches(sl);
#if VERBOSE
                    MessageBox.Show("csv matches count :"+sl.Count.ToString());
#endif

                    returnlist.Add(si);
                   
                }
#if VERBOSE
                MessageBox.Show("csv loader: list<instance>.lengith " + returnlist.Count.ToString());
#endif
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