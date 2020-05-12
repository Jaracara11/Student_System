using MySql.Data.MySqlClient;
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
    public partial class PrintStudentsForm : Form
    {
        public PrintStudentsForm()
        {
            InitializeComponent();
        }

        STUDENT student = new STUDENT();

        private void PrintStudentsForm_Load(object sender, EventArgs e)
        {
            fillGrid(new MySqlCommand("SELECT * FROM `student`"));

            if (radioBtnNo.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        //funcion llena el datagridview
        public void fillGrid(MySqlCommand command)
        {
            //cargar gridview con data de estudiantes
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioBtnNo_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioBtnYes_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            
            
            //visualizar data en el dgv dependiendo de que usuario sea seleccionado
            MySqlCommand command;
            string query;

            //chequear si el radiobtn YES esta seleccionado
            //si el usuario desea usar rango de fecha
            if (radioBtnYes.Checked)
            {
                //obtener valor de la fecha
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                if (radioBtnMale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE `birthdate` BETWEEN '" + date1 + "' AND '" + date2 + "' AND gender = 'Male'";
                }
                else if(radioBtnFemale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE `birthdate` BETWEEN '" + date1 + "' AND '" + date2 + "' AND gender = 'Female'";
                }
                else
                {
                    query = "SELECT * FROM `student` WHERE `birthdate` BETWEEN '" + date1 + "' AND '" + date2 + "'";
                }

                command = new MySqlCommand(query);
                fillGrid(command);
            }

            else //mostrar datos sin rango de fecha de nacimiento
            {
                if (radioBtnMale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE gender = 'Male'";
                }
                else if (radioBtnFemale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE gender = 'Female'";
                }
                else
                {
                    query = "SELECT * FROM `student`";
                }

                command = new MySqlCommand(query);
                fillGrid(command);
            }
        }

        //imprimir informacion del dgv a texto
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //ruta del archivo
            //nombre de archivo = students_list.pdf
            //ubicacion = desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\stundents_list.txt";

            using(var writer = new StreamWriter(path))
            {
                //verificar si el archivo existe
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString()+"\t"+"|");
                    }
                }

                writer.Close();
                MessageBox.Show("Data Exported");
            }
        }
    }
}
