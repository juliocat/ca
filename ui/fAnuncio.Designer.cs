namespace ca.ui
{
    partial class fAnuncio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fAnuncio));
            this.wbAn = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbAn
            // 
            this.wbAn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbAn.Location = new System.Drawing.Point(0, 0);
            this.wbAn.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbAn.Name = "wbAn";
            this.wbAn.Size = new System.Drawing.Size(632, 446);
            this.wbAn.TabIndex = 4;
            this.wbAn.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbAn_DocumentCompleted);
            // 
            // fAnuncio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.wbAn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fAnuncio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Announcement Board";
            this.Load += new System.EventHandler(this.fAnuncio_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbAn;

    }
}