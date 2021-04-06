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
    public partial class PrintCoursesForm : Form
    {
        public PrintCoursesForm()
        {
            InitializeComponent();
        }

        COURSE course = new COURSE();

        private void PrintCoursesForm_Load(object sender, EventArgs e)
        {
            //llenar dgv con los cursos
            dataGridView1.DataSource = course.GetAllCourses();
            //evita que aparezca una fila adicional en el dgv
            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //imprimir informacion del dgv a texto
            //ruta del archivo
            //nombre de archivo = courses_list.txt
            //ubicacion = desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\courses_list.txt";

            using (var writer = new StreamWriter(path))
            {
                //verificar si el archivo existe
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                //filas
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //columnas
                    for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
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
