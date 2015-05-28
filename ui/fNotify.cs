using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ca.CoreApp;

namespace ca.ui
{
    public partial class fNotify : Form
    {
        private int i = 4;
        const int MIN = 0;

        //animacion
        int Desde;
        int Hasta;
        int ix = 30;

        public fNotify()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void timAlert_Tick(object sender, EventArgs e)
        {
            i--;
            if (i == MIN)
            {
                this.Dispose();
            }
            else
            {
                this.btnClose.Text = "Close [" + i.ToString() + "]";
                this.PlaySound();
            }
        }

        private void PlaySound()
        {
            string alertPath = Application.StartupPath + "\\sound\\alert\\" + "alert1";
            this.Sonar(alertPath);
        }

        public void Sonar(string fname)
        {
            if (fname.Length != 0)
            {
                WINMM m = new WINMM();
                m.Play(fname);
            }
        }

        private void fNotify_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            //this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Hasta = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Desde = Screen.PrimaryScreen.WorkingArea.Height;
            this.timAlert.Enabled = true;
            ix = this.Desde;
        }

        private void timAnim_Tick(object sender, EventArgs e)
        {
            ix = ix - 30;
            this.Top = ix;

            if (ix < this.Hasta)
            {
                timAnim.Enabled = false;
            }
        }

        public void IncluirMensaje(string msg)
        {
            this.Text = "Time: " + msg;
        }
    }
}
