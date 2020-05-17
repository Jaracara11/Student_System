using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_System
{
    class COURSE
    {
        DefaultDB db = new DefaultDB();

        public bool insertCourse(string courseName, int hoursNumber, string description)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `course`(`label`, `hours_number`, `description`) VALUES (@name, @hrs, @dscr)", db.getConnection);

            //@name, @hrs, @dscr
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@hrs", MySqlDbType.Int32).Value = hoursNumber;
            command.Parameters.Add("@dscr", MySqlDbType.Text).Value = description;

            db.openConnection();

            if(command.ExecuteNonQuery() == 1)
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

        //funcion para verificar si el curso ya existe en la db
        //excluir los cursos de la verificacion cuando los editemos
        public bool checkCourseName(string courseName, int courseId = 0)
        {           
            MySqlCommand command = new MySqlCommand("SELECT * FROM `course` WHERE `label` = @cName AND id <> @cid", db.getConnection);

            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;
            command.Parameters.Add("@cName", MySqlDbType.VarChar).Value = courseName;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                db.closeConnection();
                //devuelve false si el curso ya existe
                return false;
            }
            else
            {
                db.closeConnection();
                return true;
            }
        }

        //funcion para remover curso por ID
        public bool deleteCourse(int courseId)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `course` WHERE `id` = @CID", db.getConnection);

            command.Parameters.Add("@CID", MySqlDbType.Int32).Value = courseId;

            db.openConnection();

            if(command.ExecuteNonQuery() == 1)
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

        //funcion para obtener todos los cursos
        public DataTable getAllCourses()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `course`", db.getConnection);
           
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

        //funcion para obtener un curso por ID
        public DataTable getCourseById(int courseID)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `course` WHERE id = @cid", db.getConnection);

            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseID;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

        //funcion para editar curso seleccionado
        public bool updateCourse(int courseId, string courseName, int hoursNumber, string description)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `course` SET `label` = @name, `hours_number` = @hrs, `description` = @dscr WHERE `id` = @cid", db.getConnection);

            //@cid, @name, @hrs, @dscr
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@hrs", MySqlDbType.Int32).Value = hoursNumber;
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

        //funcion ejecutar conteo queries
        public string execCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, db.getConnection);

            db.openConnection();
            string count = command.ExecuteScalar().ToString();
            db.closeConnection();

            return count;
        }

        //obtener total de cursos
        public string totalCourses()
        {
            return execCount("SELECT COUNT(*) FROM `course`");
        }
    }
}
