using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ca.ui
{
    public partial class fImgViewr : Form
    {
        public fImgViewr()
        {
            InitializeComponent();
        }

        private void fImgViewr_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        public void SetImagen(Image im)
        {
            this.picPreview.Image = im;
            this.picPreview.ZoomFit();
            this.trackDc.Value = Convert.ToInt32(this.picPreview.Zoom);
        }

        private void trackDc_ValueChanged(object sender, EventArgs e)
        {
            this.picPreview.Zoom = this.trackDc.Value;
        }

        private void picPreview_DoubleClick(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
