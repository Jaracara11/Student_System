using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Student_System
{
    class DefaultDB
    {
        //instanciamos la conexion a la db
        private MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=1234;database=studentsdb");

        //funcion que devuelve la conexion
        public MySqlConnection getConnection
        {
            get
            {
                return conn;
            }
        }

        //funcion abre la conexion
        public void openConnection()
        {
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        //funcion cierra la conexion
        public void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
