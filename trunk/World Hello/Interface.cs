//#define VERBOSE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace World_Hello
{
    public partial class Interface : Form
    {
        static songinstance currentinstance;
        static List<songinstance> instancelist;

        private String FilePath;
        private Connector conn = new Connector();
        private SongList sl;
        public Interface()
        {
            InitializeComponent();
            instancelist = csvmanager.load();
            if (instancelist.Count > 0)
            { currentinstance = instancelist[instancelist.Count - 1]; }//the last songinstance.
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Rocker Up
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Rocker Down
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click(object sender, EventArgs e) //exit
        {
            instancelist.Add(currentinstance);
            csvmanager.save(instancelist);
            Application.Exit();
        }
        private void link_RecordViaThread()
        {
            FilePath = Recorder.Record();
            //MessageBox.Show(FilePath);
        }
        private void UI_recordBtn_Click(object sender, EventArgs e)
        {
            UI_Statusbar.Text = "Recording";
            UI_Statusbar.Show();
            UI_Statusbar.Text = "Recording";
            //UI_progressBar.Show(); // needs to be repeating. No fixed end time.
            currentinstance = new songinstance();
            instancelist.Add(currentinstance);
            currentinstance.record();
            recordtime.Text = currentinstance.getrecordtime().Hour + ":" + currentinstance.getrecordtime().Minute + " " + currentinstance.getrecordtime().DayOfWeek;
            //DynamicRecord.start();
            //Thread record = new Thread(link_RecordViaThread);
            //record.Name = "recorder";
            //record.IsBackground = true;
            //record.Start();
            //Recordtimer.Enabled = true;
        }

        private void Recordtimer_Tick(object sender, EventArgs e)
        {
            if (UI_progressBar.Value <= 29)
            {
                UI_progressBar.Value += 1;
            }
            UI_progressBar.Show();
            if (UI_progressBar.Value == 29)
            {
                string fingerprint = calcfinger.generate(FilePath);
                UI_Statusbar.Text = "Processing";
                if (conn.Connect(AppSettings.server) == false)
                {
                    UI_Statusbar.Text = "Connected to Server";
                    //sl.Add(new Song("Hard", "Gay", 95));
                    sl = conn.SendFingerprint(fingerprint);
                    UI_SongList uiSongList = new UI_SongList(this);
                    uiSongList.SL = sl;
                    uiSongList.Show();
                    //panel1.Show();
                    //hideResults.Show();
                    //resultLabelTitle.Show();
                    //resultLabelArtist.Show();
                    //resultLabelTitle.Text += sl.Songs[0].Title;
                    //resultLabelArtist.Text += sl.Songs[0].Artist;
                    Recordtimer.Enabled = false;
                    //SongList SongList = new SongList();
                    //SongList.Show();
                    //this.Hide();
                    //GC.Collect();
                }
                else
                {
                    UI_Statusbar.Text = "Connection to Server failed";
                }
            }
        }

        //private void hideResults_Click(object sender, EventArgs e)
        //{
        //    UI_progressBar.Hide();
        //    UI_progressBar.Value = 0;
        //    resultLabelTitle.Hide();
        //    resultLabelTitle.Text = "Title: ";
        //    resultLabelArtist.Hide();
        //    resultLabelArtist.Text = "Title: ";
        //    hideResults.Hide();
        //    panel1.Hide();
        //}

        private void playrec_Click(object sender, EventArgs e)
        {
            UI_Statusbar.Show();
            UI_Statusbar.Text = "Playing";
            string fn = currentinstance.getwavurl();
            waveplayer.PlaySound(fn);
            UI_Statusbar.Hide();
        }
        public void reload()
        {
            UI_progressBar.Hide();
            UI_progressBar.Value = 0;
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            Options opt = new Options();
            opt.Show();
        }

        private void stoprec_Click(object sender, EventArgs e)
        {
            UI_Statusbar.Text = "saving / fingerprinting";
            currentinstance.stoprecord();
        }

        private void send_Click(object sender, EventArgs e)
        {
            currentinstance.askserver(this, AppSettings.server);
        }

        private void previousinstance_Click(object sender, EventArgs e)
        {
            int i= instancelist.IndexOf(currentinstance);
            if (i - 1 >= 0)
            {
#if VERBOSE
                MessageBox.Show("going to previous instance");
#endif
                currentinstance = instancelist[i - 1];
                recordtime.Text = currentinstance.getrecordtime().Hour + ":" + currentinstance.getrecordtime().Minute + " " + currentinstance.getrecordtime().DayOfWeek;
            }
        }

        private void nextinstance_Click(object sender, EventArgs e)
        {
            int i = instancelist.IndexOf(currentinstance);
            if (i < instancelist.Count - 1)
            {
#if VERBOSE
                MessageBox.Show("going to next instance");
#endif
                currentinstance = instancelist[i + 1];
                recordtime.Text = currentinstance.getrecordtime().Hour + ":" + currentinstance.getrecordtime().Minute + " " + currentinstance.getrecordtime().DayOfWeek;
            }
        }

        private void instancedelete_Click(object sender, EventArgs e)
        {
            currentinstance.delete();
            int i = instancelist.IndexOf(currentinstance);
            instancelist.Remove(currentinstance);
            if ((i > 0) && (i < instancelist.Count - 1))
            { currentinstance = instancelist[i - 1];
            recordtime.Text = currentinstance.getrecordtime().Hour + ":" + currentinstance.getrecordtime().Minute + " " + currentinstance.getrecordtime().DayOfWeek;
            }
        }
    }
}