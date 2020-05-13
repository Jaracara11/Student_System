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
        public bool checkCourseName(string courseName)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `course` WHERE `label` = @cName", db.getConnection);

            command.Parameters.Add("@cName", MySqlDbType.VarChar).Value = courseName;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            if(table.Rows.Count > 0)
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
