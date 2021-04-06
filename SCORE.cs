using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_System
{
    class SCORE
    {
        DefaultDB db = new DefaultDB();

        //funcion para insertar nuevo score
        public bool InsertScore(int studentId, int courseId, double score, string description)
        {
            SQLiteCommand command = new SQLiteCommand("INSERT INTO score(student_id, course_id, score, description) " +
                "VALUES ('" + studentId + "','" + courseId + "', '" + score + "', '" + description + "')", db.GetConnection);

            db.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.CloseConnection();
                return true;
            }
            else
            {
                db.CloseConnection();
                return false;
            }
        }

        //funcion para verificar si un score ya fue asignado a un estudiante en el curso actual
        public bool StudentScoreExists(int studentId, int courseId)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM `score` WHERE `student_id` = '" + 
                studentId + "' AND `course_id` = '" + courseId + "'", db.GetConnection);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
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
        public DataTable GetStudentsScore()
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = db.GetConnection;
            command.CommandText = ("SELECT score.student_id, student.first_name, student.last_name, score.course_id, course.label, score.score FROM student INNER JOIN score ON student.id = score.student_id INNER JOIN course ON score.course_id = course.id");

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funcion para remover score por ID del estudiante y del curso
        public bool DeleteScore(int studentId, int courseId)
        {
            SQLiteCommand command = new SQLiteCommand("DELETE FROM `score` WHERE `student_id` = " +
                "'" + studentId + "' AND `course_id` = '" + courseId + "'", db.GetConnection);

            db.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.CloseConnection();
                return true;
            }
            else
            {
                db.CloseConnection();
                return false;
            }
        }

        //funcion para obtener promedio por curso
        public DataTable AvgScoreByCourse()
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = db.GetConnection;
            command.CommandText = ("SELECT course.label, avg(score.score) as 'Average Score' FROM course, score WHERE course.id = score.course_id GROUP BY course.label");

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funcion para obtener score de los cursos
        public DataTable GetCourseScores(int courseId)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = db.GetConnection;
            command.CommandText = ("SELECT score.student_id, student.first_name, student.last_name, score.course_id, course.label, score.score FROM student INNER JOIN score ON student.id = score.student_id INNER JOIN course ON score.course_id = course.id WHERE score.course_id =" + courseId);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funcion para obtener score de los estudiantes
        public DataTable GetStudentScores(int studentId)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = db.GetConnection;
            command.CommandText = ("SELECT score.student_id, student.first_name, student.last_name, score.course_id, course.label, score.score FROM student INNER JOIN score ON student.id = score.student_id INNER JOIN course ON score.course_id = course.id WHERE score.course_id =" + studentId);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }
    }
}
