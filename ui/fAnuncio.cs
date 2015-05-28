using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ca.ui
{
    public partial class fAnuncio : Form
    {
        public fAnuncio()
        {
            InitializeComponent();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        public void Goto(string urlGoto)
        {
            this.wbAn.Navigate(urlGoto);
        }

        private void wbAn_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void fAnuncio_Load(object sender, EventArgs e)
        {

        }
    }
}
