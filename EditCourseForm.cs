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
    public partial class EditCourseForm : Form
    {
        public EditCourseForm()
        {
            InitializeComponent();
        }

        COURSE course = new COURSE();

        private void EditCourseForm_Load(object sender, EventArgs e)
        {
            //llenar combobox con los cursos
            comboBoxCourse.DataSource = course.GetAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";

            //setear combo box null por defecto
            comboBoxCourse.SelectedItem = null;           
        }

        //funcion para llenar el combobox y seleccionar el curso actual
        public void fillCombo(int index)
        {
            comboBoxCourse.DataSource = course.GetAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";

            comboBoxCourse.SelectedIndex = index;
        }

        private void btnEditCourse_Click(object sender, EventArgs e)
        {
            try
            {
                //actualizar curso seleccionado
                string name = textBoxLabel.Text;
                int hrs = (int)numericUpDownHours.Value;
                string descr = textBoxDescription.Text;
                int id = (int)comboBoxCourse.SelectedValue;

                //verificar si el nombre esta en blanco
                if (name.Trim() != "")
                {
                    //confirmar si el nombre del curso existe y no es el actual usando el ID del curso actual
                    if (!course.CheckCourseName(name, id))
                    {
                        MessageBox.Show("This Course Name Already Exists", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (course.UpdateCourse(id, name, hrs, descr))
                    {
                        MessageBox.Show("Course Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillCombo(comboBoxCourse.SelectedIndex);
                    }
                    else
                    {
                        MessageBox.Show("Course Not Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Enter The Course Name", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("No Course Selected", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //desplegar datos del curso seleccionado
                int id = Convert.ToInt32(comboBoxCourse.SelectedValue);
                DataTable table = new DataTable();
                table = course.GetCourseById(id);
                textBoxLabel.Text = table.Rows[0][1].ToString();
                numericUpDownHours.Value = Int32.Parse(table.Rows[0][2].ToString());
                textBoxDescription.Text = table.Rows[0][3].ToString();
            }
            catch { }
        }

    }
}
