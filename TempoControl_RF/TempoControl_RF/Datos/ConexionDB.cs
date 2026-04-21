using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace TempoControl_RF.Datos
{
    public class ConexionDB
    {
        private string connectionString = "Data Source=tempo.db";

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
    }
}