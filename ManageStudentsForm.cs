using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_System
{
    public partial class ManageStudentsForm : Form
    {
        public ManageStudentsForm()
        {
            InitializeComponent();
        }

        STUDENT student = new STUDENT();

        private void ManageStudentsForm_Load(object sender, EventArgs e)
        {
            //llenar datagridview con informacion de estudiantes
            fillGrid(new MySqlCommand("SELECT * FROM `student`"));
        }

        //funcion llena el datagridview
        public void fillGrid(MySqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.DataSource = student.getStudents(command);

            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AllowUserToAddRows = false;

            //mostrar total estudiantes dependiendo de la fila dgv
            labelTotalStudents.Text = "Total Students: " + dataGridView1.Rows.Count;
        }

        //desplegar datos de estudiantes cuando se clickee el datagrid
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female")
            {
                radioButtonFemale.Checked = true;
            }
            else
            {
                radioButtonMale.Checked = true;
            }

            maskedTextBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            pictureBoxStudent.Image = Image.FromStream(picture);
        }

        //limpia todos los campos
        private void btnReset_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxLname.Text = "";
            maskedTextBoxPhone.Clear();
            textBoxFname.Text = "";
            textBoxAddress.Text = "";
            radioButtonMale.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBoxStudent.Image = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //codigo para busqueda de estudiantes
            string query = "SELECT * FROM `student` WHERE CONCAT(`first_name`, `last_name`, `address`) LIKE'%" + textBoxSearch.Text +"%'";
            MySqlCommand command = new MySqlCommand(query);
            fillGrid(command);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //cargar imagen desde PC
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg; *.png)|*.jpg; *.png";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image = Image.FromFile(opf.FileName);
            }
        }

        //salvar la imagen en la computadora
        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();

            //colocar nombre de archivo
            svf.FileName = "Student_" + textBoxID.Text;

            //revisar si el picturebox esta vacio
            if (pictureBoxStudent.Image == null)
            {
                MessageBox.Show("No image in the picture");
            }
            else if(svf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image.Save(svf.FileName + ("." + ImageFormat.Png.ToString()));
            }
        }

        private void btnUpload_Click_1(object sender, EventArgs e)
        {
            //cargar imagen desde PC
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg; *.png)|*.jpg; *.png";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image = Image.FromFile(opf.FileName);
            }
        }

        //agregar nuevo estudiante
        private void btnAdd_Click(object sender, EventArgs e)
        {
            STUDENT student = new STUDENT();
            string fname = textBoxFname.Text;
            string lname = textBoxLname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = maskedTextBoxPhone.Text;
            string address = textBoxAddress.Text;
            string gender = "Male";

            if (radioButtonFemale.Checked)
            {
                gender = "Female";
            }

            MemoryStream pic = new MemoryStream();

            //la edad del estudiante debe ser entre 10 y 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            if ((this_year - born_year) < 10)
            {
                MessageBox.Show("The student age most be over 10", "Invalid birthdate", MessageBoxButtons.OK);
            }
            else if ((this_year - born_year) >= 100)
            {
                MessageBox.Show("The student age most be below 100", "Invalid birthdate", MessageBoxButtons.OK);
            }
            else if (verifyData())
            {
                pictureBoxStudent.Image.Save(pic, pictureBoxStudent.Image.RawFormat);

                if (student.insertStudent(fname, lname, bdate, phone, gender, address, pic))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillGrid(new MySqlCommand("SELECT * FROM `student`"));
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Fields", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //editar estudiante seleccionado
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //editar estudiante seleccionado
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                string fname = textBoxFname.Text;
                string lname = textBoxLname.Text;
                DateTime bdate = dateTimePicker1.Value;
                string phone = maskedTextBoxPhone.Text;
                string address = textBoxAddress.Text;
                string gender = "Male";

                if (radioButtonFemale.Checked)
                {
                    gender = "Female";
                }

                MemoryStream pic = new MemoryStream();

                //la edad del estudiante debe ser entre 10 y 100
                int born_year = dateTimePicker1.Value.Year;
                int this_year = DateTime.Now.Year;

                if ((this_year - born_year) < 10)
                {
                    MessageBox.Show("The student age most be over 10", "Invalid birthdate", MessageBoxButtons.OK);
                }
                else if ((this_year - born_year) >= 100)
                {
                    MessageBox.Show("The student age most be below 100", "Invalid birthdate", MessageBoxButtons.OK);
                }
                else if (verifyData())
                {
                    pictureBoxStudent.Image.Save(pic, pictureBoxStudent.Image.RawFormat);

                    if (student.updateStudent(id, fname, lname, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Student Information Updated", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid(new MySqlCommand("SELECT * FROM `student`"));
                    }
                    else
                    {
                        MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Empty Fields", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter a valid student ID", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //borrar estudiante seleccionado
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //borrar estudiante seleccionado
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                //mostrar mensaje de confirmacion borrado
                if (MessageBox.Show("Are you sure you want to delete this student?", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {
                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //la linea "fillGrid" es para que se actualice el gv al modificarlo
                        fillGrid(new MySqlCommand("SELECT * FROM `student`"));
                        //limpiar campos
                        textBoxID.Text = "";
                        textBoxFname.Text = "";
                        textBoxLname.Text = "";
                        maskedTextBoxPhone.Clear();
                        textBoxAddress.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBoxStudent.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Student Not Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter a valid student ID", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //funcion para verificar data
        bool verifyData()
        {
            if ((textBoxFname.Text.Trim() == "") ||
                (textBoxLname.Text.Trim() == "") ||
                (maskedTextBoxPhone.Text.Trim() == "") ||
                (textBoxAddress.Text.Trim() == "") ||
                (pictureBoxStudent.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
