using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SQLite;

namespace Student_System
{
    class DefaultDB
    {
        //instanciamos la conexion a la db
        private SQLiteConnection conn = new SQLiteConnection(@"datasource=C:\Users\u29230\Documents\Visual Studio 2019\studentsDB.db");

        //funcion que devuelve la conexion
        public SQLiteConnection GetConnection
        {
            get
            {
                return conn;
            }
        }

        //funcion abre la conexion
        public void OpenConnection()
        {
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        //funcion cierra la conexion
        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
