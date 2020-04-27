﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm flogin = new LoginForm();

            if (flogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm());
            }
            else
            {
                Application.Exit();
            }

        }
    }
}
