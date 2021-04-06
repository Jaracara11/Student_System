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
    public partial class RemoveCourseForm : Form
    {
        public RemoveCourseForm()
        {
            InitializeComponent();
        }

        private void btnRemoveCourse_Click(object sender, EventArgs e)
        {
            try
            {
                int courseId = Convert.ToInt32(textBoxCourseID.Text);

                COURSE course = new COURSE();

                if (MessageBox.Show("Do you want to remove this course", "Delete Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.DeleteCourse(courseId))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }
    }
}
