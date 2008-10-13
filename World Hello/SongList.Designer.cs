namespace mmoments
{
    partial class SongList
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
            this.SuspendLayout();
            // 
            // UI_ListBox
            // 
            this.UI_ListBox.Location = new System.Drawing.Point(143, 0);
            this.UI_ListBox.Name = "UI_ListBox";
            this.UI_ListBox.Size = new System.Drawing.Size(96, 254);
            this.UI_ListBox.TabIndex = 0;
            this.UI_ListBox.SelectedIndexChanged += new System.EventHandler(this.UI_ListBox_SelectedIndexChanged_1);
            // 
            // SongList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.UI_ListBox);
            this.Menu = this.mainMenu1;
            this.Name = "SongList";
            this.Text = "SongList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox UI_ListBox;
    }
}