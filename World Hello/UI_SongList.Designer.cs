namespace World_Hello
{
    partial class UI_SongList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.UI_ListBox = new System.Windows.Forms.ListBox();
            this.hideResults = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.resultLabelArtist = new System.Windows.Forms.Label();
            this.resultLabelTitle = new System.Windows.Forms.Label();
            this.resultTitleDisplay = new System.Windows.Forms.Label();
            this.resultArtistDisplay = new System.Windows.Forms.Label();
            this.resultPercent = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_ListBox
            // 
            this.UI_ListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(238)))), ((int)(((byte)(30)))));
            this.UI_ListBox.ForeColor = System.Drawing.Color.White;
            this.UI_ListBox.Location = new System.Drawing.Point(145, 7);
            this.UI_ListBox.Name = "UI_ListBox";
            this.UI_ListBox.Size = new System.Drawing.Size(88, 254);
            this.UI_ListBox.TabIndex = 0;
            this.UI_ListBox.SelectedIndexChanged += new System.EventHandler(this.UI_ListBox_SelectedIndexChanged_1);
            // 
            // hideResults
            // 
            this.hideResults.Location = new System.Drawing.Point(6, 241);
            this.hideResults.Name = "hideResults";
            this.hideResults.Size = new System.Drawing.Size(130, 20);
            this.hideResults.TabIndex = 3;
            this.hideResults.Text = "Close";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(212)))), ((int)(((byte)(244)))));
            this.panel1.Controls.Add(this.resultArtistDisplay);
            this.panel1.Controls.Add(this.resultTitleDisplay);
            this.panel1.Controls.Add(this.resultLabelArtist);
            this.panel1.Controls.Add(this.resultLabelTitle);
            this.panel1.Location = new System.Drawing.Point(3, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 108);
            // 
            // resultLabelArtist
            // 
            this.resultLabelArtist.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.resultLabelArtist.Location = new System.Drawing.Point(0, 44);
            this.resultLabelArtist.Name = "resultLabelArtist";
            this.resultLabelArtist.Size = new System.Drawing.Size(136, 20);
            this.resultLabelArtist.Text = "Artist: ";
            // 
            // resultLabelTitle
            // 
            this.resultLabelTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.resultLabelTitle.Location = new System.Drawing.Point(0, 0);
            this.resultLabelTitle.Name = "resultLabelTitle";
            this.resultLabelTitle.Size = new System.Drawing.Size(133, 20);
            this.resultLabelTitle.Text = "Title: ";
            // 
            // resultTitleDisplay
            // 
            this.resultTitleDisplay.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.resultTitleDisplay.Location = new System.Drawing.Point(3, 24);
            this.resultTitleDisplay.Name = "resultTitleDisplay";
            this.resultTitleDisplay.Size = new System.Drawing.Size(133, 20);
            // 
            // resultArtistDisplay
            // 
            this.resultArtistDisplay.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.resultArtistDisplay.Location = new System.Drawing.Point(1, 68);
            this.resultArtistDisplay.Name = "resultArtistDisplay";
            this.resultArtistDisplay.Size = new System.Drawing.Size(136, 20);
            // 
            // resultPercent
            // 
            this.resultPercent.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold);
            this.resultPercent.Location = new System.Drawing.Point(8, 122);
            this.resultPercent.Name = "resultPercent";
            this.resultPercent.Size = new System.Drawing.Size(127, 83);
            // 
            // UI_SongList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(212)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.resultPercent);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UI_ListBox);
            this.Controls.Add(this.hideResults);
            this.ForeColor = System.Drawing.Color.White;
            this.Menu = this.mainMenu1;
            this.Name = "UI_SongList";
            this.Text = "SongList";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox UI_ListBox;
        private System.Windows.Forms.Button hideResults;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label resultLabelArtist;
        private System.Windows.Forms.Label resultLabelTitle;
        private System.Windows.Forms.Label resultArtistDisplay;
        private System.Windows.Forms.Label resultTitleDisplay;
        private System.Windows.Forms.Label resultPercent;
    }
}