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
            StaticsForm staticsF = new StaticsForm();
            staticsF.Show(this);
        }

        private void editRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm upDelStdF = new UpdateDeleteStudentForm();
            upDelStdF.Show(this);
        }

        private void manageStudentsFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageStudentsForm mngStdF = new ManageStudentsForm();
            mngStdF.Show(this);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintStudentsForm prStdF = new PrintStudentsForm();
            prStdF.Show(this);
        }

        private void addCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCourseForm addCrsF = new AddCourseForm();
            addCrsF.Show(this);
        }

        private void removeCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveCourseForm rmvCrsF = new RemoveCourseForm();
            rmvCrsF.Show(this);
        }

        private void editCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCourseForm editCrsF = new EditCourseForm();
            editCrsF.Show(this);
        }

        private void manageCoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageCoursesForm mngCrsF = new ManageCoursesForm();
            mngCrsF.Show(this);
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintCoursesForm prCrsF = new PrintCoursesForm();
            prCrsF.Show(this);
        }

        private void addScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddScoreForm addScrF = new AddScoreForm();
            addScrF.Show(this);
        }

        private void removeScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveScoreForm rmvScrF = new RemoveScoreForm();
            rmvScrF.Show(this);
        }

        private void manageScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageScoreForm mngScrF = new ManageScoreForm();
            mngScrF.Show(this);
        }

        private void avgScoreByCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

    }
}
