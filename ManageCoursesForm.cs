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

        int pos;

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

            //de-seleccionar objeto del listbox
            listBoxCourses.SelectedItem = null;

            //desplegar el total de cursos
            labelTotalCourses.Text = "Total Courses: " + course.totalCourses();
        }

        //funcion para desplegar datos del curso dependiendo del indice
        public void showData(int index)
        {
            DataRow dr = course.getAllCourses().Rows[index];

            //sombrea el curso marcado
            listBoxCourses.SelectedIndex = index;

            textBoxID.Text = dr.ItemArray[0].ToString();
            textBoxLabel.Text = dr.ItemArray[1].ToString();
            numericUpDownHours.Value = int.Parse(dr.ItemArray[2].ToString());            
            textBoxDescription.Text = dr.ItemArray[3].ToString();
        }

        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //desplegar datos del curso seleccionado
                pos = listBoxCourses.SelectedIndex;
                showData(pos);
            }
            catch { }
        }

        //boton primero
        private void btnFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            showData(0);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (pos < (course.getAllCourses().Rows.Count - 1))
            {
                pos = pos + 1;
                showData(pos);
            }
            
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                pos = pos - 1;
                showData(pos);
            }
            
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            pos = course.getAllCourses().Rows.Count - 1;
            showData(pos);
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            string courseLabel = textBoxLabel.Text;
            int hours = (int)numericUpDownHours.Value;
            string description = textBoxDescription.Text;

            if (courseLabel.Trim() == "")
            {
                MessageBox.Show("Add a Course Name", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (course.checkCourseName(courseLabel))
            {
                if (course.insertCourse(courseLabel, hours, description))
                {
                    MessageBox.Show("New Course Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadListBoxData();
                }
                else
                {
                    MessageBox.Show("Course Not Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("This Course Name Already Exists", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditCourse_Click(object sender, EventArgs e)
        {
            try
            {
                //actualizar curso seleccionado
                string name = textBoxLabel.Text;
                int hrs = (int)numericUpDownHours.Value;
                string descr = textBoxDescription.Text;
                int id = Convert.ToInt32(textBoxID.Text);

                //verificar si el nombre esta en blanco
                if (name.Trim() != "")
                {
                    //confirmar si el nombre del curso existe y no es el actual usando el ID del curso actual
                    if (!course.checkCourseName(name, id))
                    {
                        MessageBox.Show("This Course Name Already Exists", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (course.updateCourse(id, name, hrs, descr))
                    {
                        MessageBox.Show("Course Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reloadListBoxData();
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

            pos = 0;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int courseId = Convert.ToInt32(textBoxID.Text);

                if (MessageBox.Show("Do you want to remove this course", "Delete Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.deleteCourse(courseId))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reloadListBoxData();
                        //limpiar campos leugo del borrado
                        textBoxID.Text = "";
                        numericUpDownHours.Value = 10;
                        textBoxLabel.Text = "";
                        textBoxDescription.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Course Not Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Enter a valid numeric ID", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            pos = 0;
        }

        /* //Esta funcion hace lo mismo que la anterior pero con clicks, la otra muestra el contenido con hoverover
        private void listBoxCourses_Click(object sender, EventArgs e)
        {
            //desplegar datos del curso seleccionado
            pos = listBoxCourses.SelectedIndex;
            showData(pos);
        }*/
    }
}
