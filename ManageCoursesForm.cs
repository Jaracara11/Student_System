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
    public partial class ManageCoursesForm : Form
    {
        public ManageCoursesForm()
        {
            InitializeComponent();
        }

        COURSE course = new COURSE();

        private void ManageCoursesForm_Load(object sender, EventArgs e)
        {
            reloadListBoxData();
        }

        //funcion que carga el listbox con los cursos
        public void reloadListBoxData()
        {
            listBoxCourses.DataSource = course.getAllCourses();
            listBoxCourses.ValueMember = "id";
            listBoxCourses.DisplayMember = "label";
        }
    }
}
