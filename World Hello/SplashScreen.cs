using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace World_Hello
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void UI_mainMenuItem1_Click(object sender, EventArgs e)
        {
            Interface UI_interface = new Interface();
            UI_interface.Show();
            this.Hide();
            GC.Collect();
            //this.Dispose();
        }

        private void UI_mainMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}