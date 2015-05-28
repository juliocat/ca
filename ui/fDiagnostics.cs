using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ca.CoreApp;
using ca.ui;

namespace ca.ui
{
    public partial class fDiagnostics : Form
    {
        public fDiagnostics()
        {
            InitializeComponent();
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //this.btnStart.Enabled = false;
            diagnos dg = new diagnos();
            dg.StartDiagnostic();
            this.chkOnError.Text = dg.informe;
        }

        private void btnStarFileAccess_Click(object sender, EventArgs e)
        {
            //this.btnStarFileAccess.Enabled = false;
            diagnos dg = new diagnos();
            config cf = new config();
            int id;

            dg.IsFileNotFoundedContinue = this.checkBox1.Checked;
            id = Convert.ToInt32(this.numericUpDown1.Value.ToString());

            //Alias
            if (radUnit.Checked == true)
            {
                dg.ProbarArchivosUnidad(id, cf);
            }
            else if (radActivity.Checked == true)
            {
                //No implementado
            }
            else if (radExer.Checked == true)
            {
                dg.ProbarArchivosEjercicio(id,cf);
            }

            this.txtFileAccessTest.Text = dg.informe;
        }
    }
}
