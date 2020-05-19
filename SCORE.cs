using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_System
{
    class SCORE
    {
        DefaultDB db = new DefaultDB();

        //funcion para insertar nuevo score
        public bool insertScore(int studentId, int courseId, double score, string description)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `score`(`student_id`, `course_id`, `score`, `description`) VALUES (@sid, @cid, @scr, @dscr)", db.getConnection);

            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;
            command.Parameters.Add("@scr", MySqlDbType.Double).Value = score;
            command.Parameters.Add("@dscr", MySqlDbType.VarChar).Value = description;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }

        //funcion para verificar si un score ya fue asignado a un estudiante en el curso actual
        public bool studentScoreExists(int studentId, int courseId)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `score` WHERE `student_id` = @sid AND `course_id` = @cid", db.getConnection);

            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count > 0)
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
