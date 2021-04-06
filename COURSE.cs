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
    class COURSE
    {
        DefaultDB db = new DefaultDB();

        public bool InsertCourse(string courseName, int hoursNumber, string description)
        {
            SQLiteCommand command = new SQLiteCommand("INSERT INTO course(label, hours_number, description) VALUES " +
                "('" + courseName + "','" + hoursNumber + "','" + description + "')", db.GetConnection);

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

        //funcion para verificar si el curso ya existe en la db
        //excluir los cursos de la verificacion cuando los editemos
        public bool CheckCourseName(string courseName, int courseId = 0)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM `course` WHERE `label` = '" + courseName +
                "' AND id != '" + courseId + "'", db.GetConnection);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                db.CloseConnection();
                //devuelve false si el curso ya existe
                return false;
            }
            else
            {
                db.CloseConnection();
                return true;
            }
        }

        //funcion para remover curso por ID
        public bool DeleteCourse(int courseId)
        {
            SQLiteCommand command = new SQLiteCommand("DELETE FROM `course` WHERE `id` = '" + courseId +
                "'", db.GetConnection);

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

        //funcion para obtener todos los cursos
        public DataTable GetAllCourses()
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM `course`", db.GetConnection);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

        //funcion para obtener un curso por ID
        public DataTable GetCourseById(int courseId)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM `course` WHERE id = '" + courseId + "'", db.GetConnection);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

        //funcion para editar curso seleccionado
        public bool UpdateCourse(int courseId, string courseName, int hoursNumber, string description)
        {
            SQLiteCommand command = new SQLiteCommand("UPDATE `course` SET `label` = '" + courseName +
                "' `hours_number` = '" + hoursNumber + "', `description` = '" + description +
                "' WHERE `id` ='" + courseId + "'", db.GetConnection);

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

        //funcion ejecutar conteo queries
        public string ExecCount(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection);

            db.OpenConnection();
            string count = command.ExecuteScalar().ToString();
            db.CloseConnection();

            return count;
        }

        //obtener total de cursos
        public string TotalCourses()
        {
            return ExecCount("SELECT COUNT(*) FROM `course`");
        }
    }
}
