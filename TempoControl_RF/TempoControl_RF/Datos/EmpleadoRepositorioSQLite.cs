using System;
using System.Text;
using System.Collections.Generic;
using TempoControl_RF.Modelos;
using Microsoft.Data.Sqlite;

namespace TempoControl_RF.Datos
{
    public class EmpleadoRepositorioSQLite : IEmpleadoRepositorio
    {
        private ConexionDB conexion = new ConexionDB();

        public EmpleadoRepositorioSQLite()
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS Empleados (
                    Id INTEGER PRIMARY KEY,
                    NombreCompleto TEXT,
                    Departamento TEXT,
                    Posicion TEXT,
                    Activo INTEGER
                );";

                cmd.ExecuteNonQuery();
            }
        }

        public void Agregar(Empleado e)
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Empleados 
                    (Id, NombreCompleto, Departamento, Posicion, Activo)
                    VALUES (@id, @nom, @dep, @pos, @act)";

                cmd.Parameters.AddWithValue("@id", e.Id);
                cmd.Parameters.AddWithValue("@nom", e.NombreCompleto);
                cmd.Parameters.AddWithValue("@dep", e.Departamento);
                cmd.Parameters.AddWithValue("@pos", e.Posicion);
                cmd.Parameters.AddWithValue("@act", e.Activo ? 1 : 0);

                cmd.ExecuteNonQuery();
            }
        }

        public Empleado ObtenerPorId(int id)
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Empleados WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Empleado
                        {
                            Id = reader.GetInt32(0),
                            NombreCompleto = reader.GetString(1),
                            Departamento = reader.GetString(2),
                            Posicion = reader.GetString(3),
                            Activo = reader.GetInt32(4) == 1
                        };
                    }
                }
            }
            return null;
        }

        public List<Empleado> ObtenerTodos()
        {
            var lista = new List<Empleado>();

            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Empleados";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Empleado
                        {
                            Id = reader.GetInt32(0),
                            NombreCompleto = reader.GetString(1),
                            Departamento = reader.GetString(2),
                            Posicion = reader.GetString(3),
                            Activo = reader.GetInt32(4) == 1
                        });
                    }
                }
            }

            return lista;
        }

        public void Actualizar(Empleado e)
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE Empleados 
                    SET NombreCompleto=@nom, Departamento=@dep, Posicion=@pos 
                    WHERE Id=@id";

                cmd.Parameters.AddWithValue("@id", e.Id);
                cmd.Parameters.AddWithValue("@nom", e.NombreCompleto);
                cmd.Parameters.AddWithValue("@dep", e.Departamento);
                cmd.Parameters.AddWithValue("@pos", e.Posicion);

                cmd.ExecuteNonQuery();
            }
        }

        public void Desactivar(int id)
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Empleados SET Activo = 0 WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
        public void Activar(int id)
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Empleados SET Activo = 1 WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}