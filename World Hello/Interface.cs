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

        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Hello World");
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World");
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}