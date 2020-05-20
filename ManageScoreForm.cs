using MySql.Data.MySqlClient;
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
    public partial class ManageScoreForm : Form
    {
        public ManageScoreForm()
        {
            InitializeComponent();
        }

        SCORE score = new SCORE();
        STUDENT student = new STUDENT();
        COURSE course = new COURSE();
        string data = "score";

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            //llenar combobox con los cursos
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";

            //llenar dgv con el score de los estudiantes
            dataGridView1.DataSource = score.getStudentsScore();
        }

        //mostrar datos de estudiantes en dgv
        private void btnShowStudents_Click(object sender, EventArgs e)
        {
            data = "student";
            MySqlCommand command = new MySqlCommand("SELECT `id`, `first_name`, `last_name`, `birthdate` FROM `student`");
            dataGridView1.DataSource = student.getStudents(command);
        }

        //mostrar score en dgv
        private void btnShowScores_Click(object sender, EventArgs e)
        {
            data = "score";
            dataGridView1.DataSource = score.getStudentsScore();
        }

        //obtener datos del dgv
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            getDataFromDGV();
        }

        //funcion para obtener datos del dgv
        public void getDataFromDGV()
        {
            //si el usuario selecciona datos de estudiante, solo se mostrara el ID
            if (data == "student")
            {
                textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            //si el usuario selecciona datos de estudiante, se mostrara el ID + curso del combobox
            else if (data == "score")
            {
                textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                comboBoxCourse.SelectedValue = dataGridView1.CurrentRow.Cells[3].Value;
            }   
        }
    }
}
