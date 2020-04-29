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
    public partial class StaticsForm : Form
    {
        public StaticsForm()
        {
            InitializeComponent();
        }

        //variables colores
        Color panTotalColor;
        Color panMaleColor;
        Color panFemaleColor;

        private void StaticsForm_Load(object sender, EventArgs e)
        {
            panTotalColor = panelTotal.BackColor;
            panMaleColor = panelMale.BackColor;
            panFemaleColor = panelFemale.BackColor;
        }

        private void labelTotal_MouseEnter(object sender, EventArgs e)
        {
            panelTotal.BackColor = Color.White;
            labelTotal.ForeColor = panTotalColor;
        }

        private void labelTotal_MouseLeave(object sender, EventArgs e)
        {
            panelTotal.BackColor = panTotalColor;
            labelTotal.ForeColor = Color.White;
        }

        private void labelMale_MouseEnter(object sender, EventArgs e)
        {

        }

        private void labelMale_MouseLeave(object sender, EventArgs e)
        {

        }

        private void labelFemale_MouseEnter(object sender, EventArgs e)
        {

        }

        private void labelFemale_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
