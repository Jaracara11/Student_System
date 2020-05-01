using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_System
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            //cargar imagen desde PC
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg; *.png)|*.jpg; *.png";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image = Image.FromFile(opf.FileName);
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            //añadir nuevo estudiante
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

        //boton cancelar
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {

        }

        private void textBoxFname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxLname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBoxPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxStudent_Click(object sender, EventArgs e)
        {

        }
    }
}
