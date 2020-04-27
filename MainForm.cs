using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_System
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void addNewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudentForm addStdF = new AddStudentForm();
            addStdF.Show(this);
        }

        private void studentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentListForm stdListF = new StudentListForm();
            stdListF.Show(this);
        }

        private void staticsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageStudentsFormToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Main_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
