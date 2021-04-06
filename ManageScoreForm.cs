using MySql.Data.MySqlClient;
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
            comboBoxCourse.DataSource = course.GetAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";

            //llenar dgv con el score de los estudiantes
            dataGridView1.DataSource = score.GetStudentsScore();
        }

        //mostrar datos de estudiantes en dgv
        private void btnShowStudents_Click(object sender, EventArgs e)
        {
            data = "student";
            SQLiteCommand command = new SQLiteCommand("SELECT `id`, `first_name`, `last_name`, `birthdate` FROM `student`");
            dataGridView1.DataSource = student.GetStudents(command);
        }

        //mostrar score en dgv
        private void btnShowScores_Click(object sender, EventArgs e)
        {
            data = "score";
            dataGridView1.DataSource = score.GetStudentsScore();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //agregar nuevo score
                int studentId = Convert.ToInt32(textBoxStudentID.Text);
                int courseId = Convert.ToInt32(comboBoxCourse.SelectedValue);
                double scoreValue = Convert.ToDouble(textBoxScore.Text);
                string description = textBoxDescription.Text;

                //funcion para verificar si un score ya fue asignado a un estudiante en el curso actual
                if (score.StudentScoreExists(studentId, courseId))
                {
                    if (score.InsertScore(studentId, courseId, scoreValue, description))
                    {
                        MessageBox.Show("Student Score Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = score.GetStudentsScore();
                    }
                    else
                    {
                        MessageBox.Show("Student Score Not Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("The Score For This Course Is Already Set", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //remover score seleccionado
            int studentId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            int courseId = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());

            if (MessageBox.Show("Do you want to delete this score?", "Remove Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (score.DeleteScore(studentId, courseId))
                {
                    MessageBox.Show("Score Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = score.GetStudentsScore();
                }
                else
                {
                    MessageBox.Show("Score Not Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnAvgCourse_Click(object sender, EventArgs e)
        {
            AvgScoreByCourseForm avgScrByCrsF = new AvgScoreByCourseForm();
            avgScrByCrsF.Show(this);
        }
    }
}
