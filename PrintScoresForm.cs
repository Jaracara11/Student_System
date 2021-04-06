using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_System
{
    public partial class PrintScoresForm : Form
    {
        public PrintScoresForm()
        {
            InitializeComponent();
        }

        SCORE score = new SCORE();
        COURSE course = new COURSE();
        STUDENT student = new STUDENT();

        private void PrintScoresForm_Load(object sender, EventArgs e)
        {
            //llenar dgv con datos de estudiantes
            dataGridView1.DataSource = student.GetStudents(new SQLiteCommand("SELECT `id`, `first_name`, `last_name` FROM `student`"));

            //llenar dgv con datos de score
            DataGridViewStudentsScore.DataSource = score.GetStudentsScore();

            //llenar dgv con datos de cursos
            listBoxCourses.DataSource = course.GetAllCourses();
            listBoxCourses.DisplayMember = "label";
            listBoxCourses.ValueMember = "id";

        }

        //cuando se seleccione un curso del listbox todos los scores asignados a ese curso se desplegaran
        private void listBoxCourses_Click(object sender, EventArgs e)
        {
            DataGridViewStudentsScore.DataSource = score.GetCourseScores(int.Parse(listBoxCourses.SelectedValue.ToString()));
        }

        //desplegar el score del estudiante seleccionado
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            DataGridViewStudentsScore.DataSource = score.GetCourseScores(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
        }

        private void labelReset_Click(object sender, EventArgs e)
        {
            //reset datos de los score
            DataGridViewStudentsScore.DataSource = score.GetStudentsScore();
        }

        //imprimir datos de scores del dgv a texto
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //imprimir informacion del dgv a texto
            //ruta del archivo
            //nombre de archivo = scores_list.txt
            //ubicacion = desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\scores_list.txt";

            using (var writer = new StreamWriter(path))
            {
                //verificar si el archivo existe
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                //filas
                for (int i = 0; i < DataGridViewStudentsScore.Rows.Count; i++)
                {
                    //columnas
                    for (int j = 0; j < DataGridViewStudentsScore.Columns.Count; j++)
                    {
                        writer.Write("\t" + DataGridViewStudentsScore.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                    }
                    //generar nueva linea
                    writer.WriteLine("");
                    //separacion
                    writer.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }

                writer.Close();
                MessageBox.Show("Data Exported");
            }
        }
    }
}
