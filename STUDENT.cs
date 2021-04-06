using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Student_System
{
    class STUDENT
    {
        DefaultDB db = new DefaultDB();

        //funcion agregar estudiantes a la db
        public bool insertStudent(string fname, string lname, DateTime bdate, 
            string phone, string gender, string address, MemoryStream picture)
        {
            SQLiteCommand command = new SQLiteCommand("INSERT INTO `studentsDB`.`student` " +
                "(first_name, last_name, birthdate, gender, phone, address, picture) " +
                "VALUES ('" + fname + "', '" + lname + "', '" + bdate + "', '" + phone + "'" +
                ", '" + gender + "', '" + address + "', '" + picture + "')", db.GetConnection);

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

        //funcion retornar tabla con data de estudiantes
        public DataTable GetStudents(SQLiteCommand command)
        {
            command.Connection = db.GetConnection;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funcion actualizar informacion de estudiantes
        public bool UpdateStudent(int id, string fname, string lname, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            SQLiteCommand command = new SQLiteCommand("UPDATE `student` SET `first_name` = '" + 
                fname + "', `last_name` = '" + lname + "', `birthdate` = '" + 
                bdate + "', `gender` = '" + gender + "', `phone` = '" + phone + "', `address` = '" + 
                address + "', `picture` = '" + picture + "' WHERE `id` = '" + id + "'", db.GetConnection);

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

        //funcion borrar estudiante seleccionado
        public bool DeleteStudent(int id)
        {
            SQLiteCommand command = new SQLiteCommand("DELETE FROM `student` WHERE `id` = '" + id +"'", db.GetConnection);
           
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

        //obtener total de estudiantes
        public string TotalStudents()
        {
            return ExecCount("SELECT COUNT(*) FROM `student`");
        }

        //obtener total de estudiantes varones
        public string TotalMaleStudents()
        {
            return ExecCount("SELECT COUNT(*) FROM `student` WHERE `gender` = 'Male'");
        }

        //obtener total de estudiantes femeninas
        public string TotalFemaleStudents()
        {
            return ExecCount("SELECT COUNT(*) FROM `student` WHERE `gender` = 'Female'");
        }

    }
}
