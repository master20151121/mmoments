using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace World_Hello
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            UI_serverAddress.Text = AppSettings.server;
        }

        private void UI_optionsOK_Click(object sender, EventArgs e)
        {
            AppSettings.server = UI_serverAddress.Text;
            this.Close();
        }
    }
}