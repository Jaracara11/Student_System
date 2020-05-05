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
    }
}
