namespace World_Hello
{
    partial class Options
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
            this.UI_optionsOK = new System.Windows.Forms.Button();
            this.UI_optionsServer = new System.Windows.Forms.Label();
            this.UI_serverAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // UI_optionsOK
            // 
            this.UI_optionsOK.BackColor = System.Drawing.Color.Lime;
            this.UI_optionsOK.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.UI_optionsOK.Location = new System.Drawing.Point(2, 236);
            this.UI_optionsOK.Name = "UI_optionsOK";
            this.UI_optionsOK.Size = new System.Drawing.Size(79, 29);
            this.UI_optionsOK.TabIndex = 0;
            this.UI_optionsOK.Text = "OK";
            this.UI_optionsOK.Click += new System.EventHandler(this.UI_optionsOK_Click);
            // 
            // UI_optionsServer
            // 
            this.UI_optionsServer.Location = new System.Drawing.Point(2, 0);
            this.UI_optionsServer.Name = "UI_optionsServer";
            this.UI_optionsServer.Size = new System.Drawing.Size(79, 21);
            this.UI_optionsServer.Text = "Server:";
            // 
            // UI_serverAddress
            // 
            this.UI_serverAddress.Location = new System.Drawing.Point(3, 24);
            this.UI_serverAddress.Name = "UI_serverAddress";
            this.UI_serverAddress.Size = new System.Drawing.Size(234, 21);
            this.UI_serverAddress.TabIndex = 9;
            this.UI_serverAddress.Text = "127.0.0.1";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(212)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.UI_serverAddress);
            this.Controls.Add(this.UI_optionsServer);
            this.Controls.Add(this.UI_optionsOK);
            this.Menu = this.mainMenu1;
            this.Name = "Options";
            this.Text = "Options";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UI_optionsOK;
        private System.Windows.Forms.Label UI_optionsServer;
        private System.Windows.Forms.TextBox UI_serverAddress;
    }
}