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
    public partial class AvgScoreByCourseForm : Form
    {
        public AvgScoreByCourseForm()
        {
            InitializeComponent();
        }

        private void AvgScoreByCourseForm_Load(object sender, EventArgs e)
        {
            //llenar dgv con el score promedio por curso
            SCORE score = new SCORE();

            dataGridView1.DataSource = score.avgScoreByCourse();
        }
    }
}
