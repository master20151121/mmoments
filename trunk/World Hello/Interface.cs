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
        private String FilePath;
        private Connector conn = new Connector();
        private SongList sl;
        public Interface()
        {
            InitializeComponent();
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

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void link_RecordViaThread()
        {
            FilePath = Recorder.Record();
            //MessageBox.Show(FilePath);
        }
        private void UI_recordBtn_Click(object sender, EventArgs e)
        {
            UI_stopBtn.Show();
            UI_Statusbar.Text = "Recording";
            UI_Statusbar.Show();
            UI_progressBar.Show();
            //DynamicRecord.start();
            Thread record = new Thread(link_RecordViaThread);
            record.Name = "recorder";
            record.IsBackground = true;
            record.Start();
            Recordtimer.Enabled = true;
        }

        private void UI_stopBtn_Click(object sender, EventArgs e)
        {
            UI_recordBtn.Show();
            UI_stopBtn.Hide();
            //DynamicRecord.stop();

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
                if (conn.Connect(serverAddress.Text) == false)
                {
                    UI_Statusbar.Text = "Connected to Server";
                    //sl.Add(new Song("Hard", "Gay", 95));
                    sl = conn.SendFingerprint(fingerprint);
                    UI_SongList uiSongList = new UI_SongList();
                    uiSongList.SL = sl;
                    uiSongList.Show();
                    panel1.Show();
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

        private void hideResults_Click(object sender, EventArgs e)
        {
            UI_progressBar.Hide();
            UI_progressBar.Value = 0;
            resultLabelTitle.Hide();
            resultLabelTitle.Text = "Title: ";
            resultLabelArtist.Hide();
            resultLabelArtist.Text = "Title: ";
            hideResults.Hide();
            panel1.Hide();
        }

        private void playrec_Click(object sender, EventArgs e)
        {
            String fileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                fileName = System.IO.Path.GetDirectoryName(fileName);
                fileName = System.IO.Path.Combine(fileName, "recording.wav");
                waveplayer.PlaySound(fileName);
        }
    }
}