using System;
using System.Collections.Generic;
using TempoControl_RF.Modelos;
using Microsoft.Data.Sqlite;

namespace TempoControl_RF.Datos
{
    public class RegistroRepositorioSQLite : IRegistroRepositorio
    {
        private ConexionDB conexion = new ConexionDB();

        public RegistroRepositorioSQLite()
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS Registros (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    EmpleadoId INTEGER,
                    FechaHoraEntrada TEXT,
                    FechaHoraSalida TEXT
                );";

                cmd.ExecuteNonQuery();
            }
        }

        public void RegistrarEntrada(RegistroFichaje r)
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Registros 
                    (EmpleadoId, FechaHoraEntrada) 
                    VALUES (@emp, @entrada)";

                cmd.Parameters.AddWithValue("@emp", r.EmpleadoId);
                cmd.Parameters.AddWithValue("@entrada", r.FechaHoraEntrada.ToString("s"));

                cmd.ExecuteNonQuery();
            }
        }

        public void RegistrarSalida(int empleadoId)
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText =
                @"UPDATE Registros 
                  SET FechaHoraSalida=@salida 
                  WHERE EmpleadoId=@emp AND FechaHoraSalida IS NULL";

                cmd.Parameters.AddWithValue("@emp", empleadoId);
                cmd.Parameters.AddWithValue("@salida", DateTime.Now.ToString("s"));

                cmd.ExecuteNonQuery();
            }
        }

        public List<RegistroFichaje> ObtenerTodos()
        {
            var lista = new List<RegistroFichaje>();

            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Registros";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new RegistroFichaje
                        {
                            Id = reader.GetInt32(0),
                            EmpleadoId = reader.GetInt32(1),
                            FechaHoraEntrada = DateTime.Parse(reader.GetString(2)),
                            FechaHoraSalida = reader.IsDBNull(3) ? null : DateTime.Parse(reader.GetString(3))
                        });
                    }
                }
            }

            return lista;
        }
    }
}