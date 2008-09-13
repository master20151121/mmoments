namespace World_Hello
{
    partial class SplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu UI_mainMenu;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.UI_mainMenu = new System.Windows.Forms.MainMenu();
            this.UI_mainMenuItem1 = new System.Windows.Forms.MenuItem();
            this.UI_mainMenuItem2 = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UI_mainMenu
            // 
            this.UI_mainMenu.MenuItems.Add(this.UI_mainMenuItem1);
            this.UI_mainMenu.MenuItems.Add(this.UI_mainMenuItem2);
            // 
            // UI_mainMenuItem1
            // 
            this.UI_mainMenuItem1.Text = "Accept";
            this.UI_mainMenuItem1.Click += new System.EventHandler(this.UI_mainMenuItem1_Click);
            // 
            // UI_mainMenuItem2
            // 
            this.UI_mainMenuItem2.Text = "Decline";
            this.UI_mainMenuItem2.Click += new System.EventHandler(this.UI_mainMenuItem2_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 268);
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label1);
            this.Menu = this.UI_mainMenu;
            this.Name = "SplashScreen";
            this.Text = "Mobile Musical Moments";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem UI_mainMenuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem UI_mainMenuItem2;

    }
}