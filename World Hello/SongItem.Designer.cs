namespace mmoments
{
    partial class SongItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UI_Listbox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // UI_Listbox1
            // 
            this.UI_Listbox1.Items.Add("Title:");
            this.UI_Listbox1.Items.Add("Artist:");
            this.UI_Listbox1.Items.Add("Match %:");
            this.UI_Listbox1.Location = new System.Drawing.Point(0, 0);
            this.UI_Listbox1.Name = "UI_Listbox1";
            this.UI_Listbox1.Size = new System.Drawing.Size(109, 74);
            this.UI_Listbox1.TabIndex = 1;
            // 
            // SongItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.UI_Listbox1);
            this.Name = "SongItem";
            this.Size = new System.Drawing.Size(110, 76);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox UI_Listbox1;

    }
}
