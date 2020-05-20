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

        //funcion para obtener score de los estudiantes
        public DataTable getStudentsScore()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = db.getConnection;
            command.CommandText = ("SELECT score.student_id, student.first_name, student.last_name, score.course_id, course.label, score.score FROM student INNER JOIN score ON student.id = score.student_id INNER JOIN course ON score.course_id = course.id");

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funcion para remover score por ID del estudiante y del curso
        public bool deleteScore(int studentId, int courseId)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `student_id` = @sid AND `course_id` = @cid", db.getConnection);

            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;

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

        //funcion para obtener promedio por curso
        public DataTable avgScoreByCourse()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = db.getConnection;
            command.CommandText = ("SELECT course.label, avg(score.score) as 'Average Score' FROM course, score WHERE course.id = score.course_id GROUP BY course.label");

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funcion para obtener score de los cursos
        public DataTable getCourseScores(int courseId)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = db.getConnection;
            command.CommandText = ("SELECT score.student_id, student.first_name, student.last_name, score.course_id, course.label, score.score FROM student INNER JOIN score ON student.id = score.student_id INNER JOIN course ON score.course_id = course.id WHERE score.course_id =" + courseId);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funcion para obtener score de los estudiantes
        public DataTable getStudentScores(int studentId)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = db.getConnection;
            command.CommandText = ("SELECT score.student_id, student.first_name, student.last_name, score.course_id, course.label, score.score FROM student INNER JOIN score ON student.id = score.student_id INNER JOIN course ON score.course_id = course.id WHERE score.course_id =" + studentId);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }
    }
}
