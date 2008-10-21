using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace World_Hello
{
    public partial class UI_SongList : Form
    {
        SongList sl = new SongList();
        Interface _par;
        public UI_SongList(Interface par)
        {
            InitializeComponent();
            _par = par;
            UI_ListBox.DataSource = sl.Songs;
            UI_ListBox.DisplayMember = "Title";
        }

        private void UI_ListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            resultArtistDisplay.Text = ((Song)UI_ListBox.SelectedItem).Artist;
            resultTitleDisplay.Text = ((Song)UI_ListBox.SelectedItem).Title;
            resultPercent.Text = ((Song)UI_ListBox.SelectedItem).Match.ToString() + "%";
        }
        public SongList SL { get { return sl; } set { sl = value; UI_ListBox.Invalidate(); } }

        private void hideResults_Click(object sender, EventArgs e)
        {
            this.Close();
            _par.reload();
        }
    }
}