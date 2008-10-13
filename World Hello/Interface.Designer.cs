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
            this.UI_background = new System.Windows.Forms.PictureBox();
            this.UI_recordBtn = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.UI_progressBar = new System.Windows.Forms.ProgressBar();
            this.UI_stopBtn = new System.Windows.Forms.Button();
            this.Recordtimer = new System.Windows.Forms.Timer();
            this.UI_Statusbar = new System.Windows.Forms.StatusBar();
            this.serverAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hideResults = new System.Windows.Forms.Button();
            this.resultLabelArtist = new System.Windows.Forms.Label();
            this.resultLabelTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_background
            // 
            this.UI_background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UI_background.Image = ((System.Drawing.Image)(resources.GetObject("UI_background.Image")));
            this.UI_background.Location = new System.Drawing.Point(0, 0);
            this.UI_background.Name = "UI_background";
            this.UI_background.Size = new System.Drawing.Size(227, 270);
            this.UI_background.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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
            // UI_stopBtn
            // 
            this.UI_stopBtn.BackColor = System.Drawing.Color.Red;
            this.UI_stopBtn.Location = new System.Drawing.Point(3, 3);
            this.UI_stopBtn.Name = "UI_stopBtn";
            this.UI_stopBtn.Size = new System.Drawing.Size(70, 30);
            this.UI_stopBtn.TabIndex = 5;
            this.UI_stopBtn.Text = "Stop";
            this.UI_stopBtn.Visible = false;
            this.UI_stopBtn.Click += new System.EventHandler(this.UI_stopBtn_Click);
            // 
            // Recordtimer
            // 
            this.Recordtimer.Interval = 1000;
            this.Recordtimer.Tick += new System.EventHandler(this.Recordtimer_Tick);
            // 
            // UI_Statusbar
            // 
            this.UI_Statusbar.Location = new System.Drawing.Point(0, 244);
            this.UI_Statusbar.Name = "UI_Statusbar";
            this.UI_Statusbar.Size = new System.Drawing.Size(227, 26);
            this.UI_Statusbar.Visible = false;
            // 
            // serverAddress
            // 
            this.serverAddress.Location = new System.Drawing.Point(79, 219);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.Size = new System.Drawing.Size(139, 25);
            this.serverAddress.TabIndex = 8;
            this.serverAddress.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.Text = "SERVER";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hideResults);
            this.panel1.Controls.Add(this.resultLabelArtist);
            this.panel1.Controls.Add(this.resultLabelTitle);
            this.panel1.Location = new System.Drawing.Point(36, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 72);
            this.panel1.Visible = false;
            // 
            // hideResults
            // 
            this.hideResults.Location = new System.Drawing.Point(43, 49);
            this.hideResults.Name = "hideResults";
            this.hideResults.Size = new System.Drawing.Size(72, 20);
            this.hideResults.TabIndex = 3;
            this.hideResults.Text = "OK";
            this.hideResults.Visible = false;
            this.hideResults.Click += new System.EventHandler(this.hideResults_Click);
            // 
            // resultLabelArtist
            // 
            this.resultLabelArtist.Location = new System.Drawing.Point(0, 20);
            this.resultLabelArtist.Name = "resultLabelArtist";
            this.resultLabelArtist.Size = new System.Drawing.Size(164, 20);
            this.resultLabelArtist.Text = "Artist: ";
            this.resultLabelArtist.Visible = false;
            // 
            // resultLabelTitle
            // 
            this.resultLabelTitle.Location = new System.Drawing.Point(0, 0);
            this.resultLabelTitle.Name = "resultLabelTitle";
            this.resultLabelTitle.Size = new System.Drawing.Size(161, 20);
            this.resultLabelTitle.Text = "Title: ";
            this.resultLabelTitle.Visible = false;
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverAddress);
            this.Controls.Add(this.UI_Statusbar);
            this.Controls.Add(this.UI_stopBtn);
            this.Controls.Add(this.UI_progressBar);
            this.Controls.Add(this.UI_recordBtn);
            this.Controls.Add(this.UI_background);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Interface";
            this.Text = "Musical Moments";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox UI_background;
        private System.Windows.Forms.Button UI_recordBtn;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ProgressBar UI_progressBar;
        private System.Windows.Forms.Button UI_stopBtn;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Timer Recordtimer;
        private System.Windows.Forms.StatusBar UI_Statusbar;
        private System.Windows.Forms.TextBox serverAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button hideResults;
        private System.Windows.Forms.Label resultLabelArtist;
        private System.Windows.Forms.Label resultLabelTitle;
    }
}

