using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace World_Hello
{
    public partial class Interface : Form
    {
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
            MessageBox.Show("Hello World");
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UI_recordBtn_Click(object sender, EventArgs e)
        {
            UI_stopBtn.Show();
            UI_recordBtn.Hide();
        }

        private void UI_stopBtn_Click(object sender, EventArgs e)
        {
            UI_recordBtn.Show();
            if (UI_progressBar.Value <= 90)
            {
                UI_progressBar.Value += 10;
            }
            UI_progressBar.Show();
            UI_stopBtn.Hide();
        }

    }
}