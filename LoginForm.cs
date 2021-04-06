using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //validacion de usuario y password login
            DefaultDB db = new DefaultDB();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            DataTable table = new DataTable();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM `users` WHERE `username` = '" + 
                textBoxUsername.Text + "' AND `password` = '" + textBoxPassword.Text + "'", db.GetConnection);

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
