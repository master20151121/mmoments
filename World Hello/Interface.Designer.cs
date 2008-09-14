namespace World_Hello
{
    partial class Interface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UI_recordBtn = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.UI_progressBar = new System.Windows.Forms.ProgressBar();
            this.UI_stopBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 294);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // UI_recordBtn
            // 
            this.UI_recordBtn.BackColor = System.Drawing.Color.LawnGreen;
            this.UI_recordBtn.Location = new System.Drawing.Point(3, 3);
            this.UI_recordBtn.Name = "UI_recordBtn";
            this.UI_recordBtn.Size = new System.Drawing.Size(70, 30);
            this.UI_recordBtn.TabIndex = 2;
            this.UI_recordBtn.Text = "Record";
            this.UI_recordBtn.Click += new System.EventHandler(this.UI_recordBtn_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Click Me";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Exit";
            // 
            // UI_progressBar
            // 
            this.UI_progressBar.Location = new System.Drawing.Point(36, 139);
            this.UI_progressBar.Name = "UI_progressBar";
            this.UI_progressBar.Size = new System.Drawing.Size(164, 20);
            this.UI_progressBar.Visible = false;
            // 
            // UI_stopBtn
            // 
            this.UI_stopBtn.BackColor = System.Drawing.Color.Red;
            this.UI_stopBtn.Location = new System.Drawing.Point(3, 3);
            this.UI_stopBtn.Name = "UI_stopBtn";
            this.UI_stopBtn.Size = new System.Drawing.Size(72, 30);
            this.UI_stopBtn.TabIndex = 5;
            this.UI_stopBtn.Text = "Stop";
            this.UI_stopBtn.Visible = false;
            this.UI_stopBtn.Click += new System.EventHandler(this.UI_stopBtn_Click);
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.UI_stopBtn);
            this.Controls.Add(this.UI_progressBar);
            this.Controls.Add(this.UI_recordBtn);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "Interface";
            this.Text = "Musical Moments";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button UI_recordBtn;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ProgressBar UI_progressBar;
        private System.Windows.Forms.Button UI_stopBtn;
    }
}

