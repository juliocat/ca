using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ca.ui
{
    public partial class fAbout : Form
    {
        string credits = "";
        int lng;
        int j=0;

        public fAbout()
        {
            InitializeComponent();
        }

        private void fAbout_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void fAbout_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Dispose();
        }

        private void fAbout_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void fAbout_Load(object sender, EventArgs e)
        {
            string ruta;
            Random r = new Random();
            int x;
             //random desde 1 hasta <n
            x = r.Next(1, 6);
            ruta = "img" + x.ToString();
            ruta = Application.StartupPath + "\\splash\\" + ruta;
            try
            {
                this.BackgroundImage = Image.FromFile(ruta);
            }
            catch
            { 
                //this.Height = 452
            }

            credits += "$>Proyecto CA -> Class Assistant\r\n";
            credits += "$>2015\r\n";
            credits += "$>--------------------------------------\r\n";
            credits += "$>Créditos\r\n";
            credits += "$>--------------------------------------\r\n";
            credits += "$>Julio Cesar Anchiraico Trujillo - julioanc@ucm.es\r\n";
            credits += "$>Ignacio Melendrez Moreto - igmelend@ucm.es\r\n";
            credits += "$>Richard Cabana Ramirez - richardjordancabana@ucm.es\r\n";
            credits += "$>--------------------------------------\r\n";
            credits += "$>░░▓█▓░░░░░░░░▄█▄░░░░░░░░░░░░░░░░░░░░▒▓▒░▒\r\n";
            credits += "$>░▓███▓░░░░░▄█████▄░░░░░░░░▒▒▒░░░▒▒░░▒▒▒▒▒\r\n";
            credits += "$>░░▓█▓░░░░░█████████░░░░▓███▒▒▒░░▒▒▒▓▒▓▒▓▒\r\n";
            credits += "$>░░░▓░░░░░░░█▓▓▓▓▓█░▒▒███████▄▒░▒▒▒▒▒▒▒▒▒▒\r\n";
            credits += "$>░░▓█▓░░░░░░░░▄█▄░░░░░░░░░░░░░░░░░░░░▒▓▒░▒\r\n";
            credits += "$>███▓████████▓▓█▓▓██████▓██▓██████████████\r\n";
            credits += "$>--------------------------------------\r\n";
            credits += "$>Gracias!\r\n";
            credits += "$><eof>\n_\n_\n_\n_\n";
            credits += "$>reload\n\n_\n_\n_";

            this.lng = credits.Length;

            timPrint.Enabled = true;
        }

        private void timPrint_Tick(object sender, EventArgs e)
        {
            j++;
            if (j >= this.lng)
            {
                this.txtCredits.Clear();
                j = 0;
            }
            string t = credits.Substring(j, 1);
            this.txtCredits.AppendText(t);
        }
    }
}
