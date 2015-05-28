namespace ca.ui
{
    partial class fImgViewr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fImgViewr));
            this.trackDc = new System.Windows.Forms.TrackBar();
            this.picPreview = new Fireball.Windows.Forms.PicturePreview();
            ((System.ComponentModel.ISupportInitialize)(this.trackDc)).BeginInit();
            this.SuspendLayout();
            // 
            // trackDc
            // 
            this.trackDc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackDc.Location = new System.Drawing.Point(0, 425);
            this.trackDc.Minimum = 1;
            this.trackDc.Name = "trackDc";
            this.trackDc.Size = new System.Drawing.Size(578, 45);
            this.trackDc.TabIndex = 3;
            this.trackDc.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackDc.Value = 1;
            this.trackDc.ValueChanged += new System.EventHandler(this.trackDc_ValueChanged);
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.picPreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Image = null;
            this.picPreview.Info = null;
            this.picPreview.Location = new System.Drawing.Point(0, 0);
            this.picPreview.Name = "picPreview";
            this.picPreview.ShowInfo = false;
            this.picPreview.Size = new System.Drawing.Size(578, 425);
            this.picPreview.TabIndex = 4;
            this.picPreview.UseHandCursor = false;
            this.picPreview.Zoom = 1F;
            this.picPreview.DoubleClick += new System.EventHandler(this.picPreview_DoubleClick);
            // 
            // fImgViewr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 470);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.trackDc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fImgViewr";
            this.Text = "Viewer";
            this.Load += new System.EventHandler(this.fImgViewr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackDc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackDc;
        private Fireball.Windows.Forms.PicturePreview picPreview;



    }
}