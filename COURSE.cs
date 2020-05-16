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
                return true;
            }
            else
            {
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
                //devuelve false si el curso ya existe
                return true;
            }
            else
            {
                return false;
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
                return true;
            }
            else
            {
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
            MySqlCommand command = new MySqlCommand("UPDATE `studentsdb`.`course` SET `label` = '" + courseName + "', `hours_number` = '" + hoursNumber + "', `description`= '" + description + "' WHERE `id` = '" + courseId + "'", db.getConnection);

            
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
