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
            this.UI_recordBtn = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.UI_progressBar = new System.Windows.Forms.ProgressBar();
            this.Recordtimer = new System.Windows.Forms.Timer();
            this.UI_Statusbar = new System.Windows.Forms.StatusBar();
            this.playrec = new System.Windows.Forms.Button();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.UI_background = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
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
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.Text = "Menu";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Record";
            this.menuItem4.Click += new System.EventHandler(this.UI_recordBtn_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Exit";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // UI_progressBar
            // 
            this.UI_progressBar.Location = new System.Drawing.Point(36, 139);
            this.UI_progressBar.Maximum = 30;
            this.UI_progressBar.Name = "UI_progressBar";
            this.UI_progressBar.Size = new System.Drawing.Size(164, 20);
            this.UI_progressBar.Visible = false;
            // 
            // Recordtimer
            // 
            this.Recordtimer.Interval = 1000;
            this.Recordtimer.Tick += new System.EventHandler(this.Recordtimer_Tick);
            // 
            // UI_Statusbar
            // 
            this.UI_Statusbar.Location = new System.Drawing.Point(0, 246);
            this.UI_Statusbar.Name = "UI_Statusbar";
            this.UI_Statusbar.Size = new System.Drawing.Size(240, 22);
            this.UI_Statusbar.Visible = false;
            // 
            // playrec
            // 
            this.playrec.Location = new System.Drawing.Point(183, 3);
            this.playrec.Name = "playrec";
            this.playrec.Size = new System.Drawing.Size(54, 25);
            this.playrec.TabIndex = 12;
            this.playrec.Text = "play";
            this.playrec.Click += new System.EventHandler(this.playrec_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Options";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // UI_background
            // 
            this.UI_background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UI_background.Image = ((System.Drawing.Image)(resources.GetObject("UI_background.Image")));
            this.UI_background.Location = new System.Drawing.Point(0, 0);
            this.UI_background.Name = "UI_background";
            this.UI_background.Size = new System.Drawing.Size(240, 268);
            this.UI_background.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.playrec);
            this.Controls.Add(this.UI_Statusbar);
            this.Controls.Add(this.UI_progressBar);
            this.Controls.Add(this.UI_recordBtn);
            this.Controls.Add(this.UI_background);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Interface";
            this.Text = "Musical Moments";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UI_recordBtn;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ProgressBar UI_progressBar;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Timer Recordtimer;
        private System.Windows.Forms.StatusBar UI_Statusbar;
        private System.Windows.Forms.Button playrec;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.PictureBox UI_background;
    }
}

