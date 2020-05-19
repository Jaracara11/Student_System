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
    public partial class AddScoreForm : Form
    {
        public AddScoreForm()
        {
            InitializeComponent();
        }

        SCORE score = new SCORE();
        COURSE course = new COURSE();
        STUDENT student = new STUDENT();
       
        private void AddScoreForm_Load(object sender, EventArgs e)
        {
            //llenar combobox con el nombre de los cursos
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";

            //llenar dgv con datos de los estudiantes (id, nombre, apellido)
            MySqlCommand command = new MySqlCommand("SELECT `id`, `first_name`, `last_name` FROM `student`");
            dataGridView1.DataSource = student.getStudents(command);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //obtener el id del estudiante seleccionado
            textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnAddScore_Click(object sender, EventArgs e)
        {
            try
            {
                //agregar nuevo score
                int studentId = Convert.ToInt32(textBoxStudentID.Text);
                int courseId = Convert.ToInt32(comboBoxCourse.SelectedValue);
                double scoreValue = Convert.ToDouble(textBoxScore.Text);
                string description = textBoxDescription.Text;

                //funcion para verificar si un score ya fue asignado a un estudiante en el curso actual
                if (score.studentScoreExists(studentId, courseId))
                {
                    if (score.insertScore(studentId, courseId, scoreValue, description))
                    {
                        MessageBox.Show("Student Score Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
