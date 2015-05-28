namespace ca.ui
{
    partial class fAbout
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fAbout));
            this.txtCredits = new System.Windows.Forms.TextBox();
            this.timPrint = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtCredits
            // 
            this.txtCredits.BackColor = System.Drawing.Color.Black;
            this.txtCredits.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtCredits.Font = new System.Drawing.Font("MS Mincho", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredits.ForeColor = System.Drawing.Color.Lime;
            this.txtCredits.Location = new System.Drawing.Point(0, 479);
            this.txtCredits.Multiline = true;
            this.txtCredits.Name = "txtCredits";
            this.txtCredits.ReadOnly = true;
            this.txtCredits.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCredits.Size = new System.Drawing.Size(640, 87);
            this.txtCredits.TabIndex = 3;
            // 
            // timPrint
            // 
            this.timPrint.Interval = 66;
            this.timPrint.Tick += new System.EventHandler(this.timPrint_Tick);
            // 
            // fAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 566);
            this.Controls.Add(this.txtCredits);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Deactivate += new System.EventHandler(this.fAbout_Deactivate);
            this.Load += new System.EventHandler(this.fAbout_Load);
            this.Click += new System.EventHandler(this.fAbout_Click);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fAbout_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCredits;
        private System.Windows.Forms.Timer timPrint;


    }
}