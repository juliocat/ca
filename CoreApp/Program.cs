using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using ca.ui;

namespace ca.CoreApp
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process[] myProcesses;
            myProcesses = System.Diagnostics.Process.GetProcessesByName("ca");

            if (myProcesses.GetLength(0) < 2)
            {
                string strRefCmd = null;
                if (ParseArgs(args, ref strRefCmd))
                {
                    Application.Run(new fMain());
                    //Application.Run(new fIntro());
                }
            }

        }
        private static bool ParseArgs(String[] args, ref string strCmd)
        {
            try
            {
                if (args.Length == 0)
                {
                    return true;
                }
                if (args[0].ToString().Length > 0)
                {
                    strCmd = args[0].ToString();
                    return true;
                }
            }
            catch
            {
                ShowUsage();
                return false;
            }

            return true;
        }
        private static void ShowUsage()
        {
            MessageBox.Show("No valid parameters");
        }
    }
}   