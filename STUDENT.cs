using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_System
{
    class STUDENT
    {
        DefaultDB db = new DefaultDB();

        //funcion agregar estudiantes a la db
        public bool insertStudent(string fname, string lname, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `studentsdb`.`student` (`first_name`, `last_name`, `birthdate`, `gender`, `phone`, `address`, `picture`) VALUES (@fn, @ln, @bdt, @gdr, @phn, @adr, @pic)", db.getConnection);

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

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

        //funcion retornar tabla con data de estudiantes
        public DataTable getStudents(MySqlCommand command)
        {
            command.Connection = db.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funcion actualizar informacion de estudiantes
        public bool updateStudent(int id, string fname, string lname, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET `first_name` = @fn, `last_name` = @ln, `birthdate` = @bdt, `gender` = @gdr, `phone` = @phn, `address` = @adr, `picture` = @pic WHERE `id` = @ID", db.getConnection);

            //@ID, @fn, @ln, @bdt, @gdr, phn, @adr, @pic
            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

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

        //funcion borrar estudiante seleccionado
        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `student` WHERE `id` = @studentID", db.getConnection);

            command.Parameters.Add("@studentID", MySqlDbType.Int32).Value = id;
            
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

        //obtener total de estudiantes
        public string totalStudents()
        {
            return execCount("SELECT COUNT(*) FROM `student`");
        }

        //obtener total de estudiantes varones
        public string totalMaleStudents()
        {
            return execCount("SELECT COUNT(*) FROM `student` WHERE `gender` = 'Male'");
        }

        //obtener total de estudiantes femeninas
        public string totalFemaleStudents()
        {
            return execCount("SELECT COUNT(*) FROM `student` WHERE `gender` = 'Female'");
        }

    }
}
