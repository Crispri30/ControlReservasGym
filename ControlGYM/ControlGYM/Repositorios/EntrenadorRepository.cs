using Microsoft.Data.SqlClient;
using ControlGYM.Modelos;
using ControlGYM.Utilidades;

namespace ControlGYM.Repositorios
{
    public class EntrenadorRepository
    {
        public void AgregarEntrenador(Entrenadores entrenador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand ("SELECT COUNT (*) WHERE EntrenadorID = @EntrenadorID", conexion))
                {
                    cmd.Parameters.AddWithValue("@EntrenadorID", entrenador.EntrenadorID);
                    int verificar = (int)cmd.ExecuteScalar();
                    if (verificar > 0)
                    {
                        throw new Exception("El entrenador ya existe en la base de datos.");
                    }
                }
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Entrenadores (EntrenadorID, Nombre, Especialidad, Disponibilidad) VALUES (@EntrenadorID, @Nombre, @Especialidad, @Disponibilidad)  ", conexion))
                {
                    cmd.Parameters.AddWithValue("@EntrenadorID", entrenador.EntrenadorID);
                    cmd.Parameters.AddWithValue("@Nombre", entrenador.Nombre);
                    cmd.Parameters.AddWithValue("@Especialidad", entrenador.Especialidad.ToString());
                    cmd.Parameters.AddWithValue("@Disponibilidad", entrenador.Disponibilidad);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ActualizarDisponibilidad(int entrenadorID, bool disponibilidad)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Entrenadores SET Disponibilidad = @Disponibilidad WHERE EntrenadorID = @EntrenadorID", conexion))
                {
                    cmd.Parameters.AddWithValue("@EntrenadorID", entrenadorID);
                    cmd.Parameters.AddWithValue("@Disponibilidad", disponibilidad);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Entrenadores> ObtenerEntrenadores()
        {
            var lista_entrenadores = new List<Entrenadores>();
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT EntrenadorID, Nombre, Especialidad, Disponibilidad FROM Entrenadores", conexion) )
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_entrenadores.Add(new Entrenadores
                            {
                                EntrenadorID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Especialidad = (Especialidad)Enum.Parse(typeof(Especialidad), reader.GetString(2)),
                                Disponibilidad = reader.GetBoolean(3)
                            });
                        }
                    }
                }
                return lista_entrenadores;
            }
        }

        public void ActualizarEntrenador(Entrenadores entrenador)
        {
            using (SqlConnection conexion  = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand ("UPDATE Entrenadores SET Nombre = @Nombre, Especialidad = @Especialidad, Disponibilidad = @Disponibilidad WHERE EntrenadorID = @EntrenadorID", conexion))
                {
                    cmd.Parameters.AddWithValue("@EntrenadorID", entrenador.EntrenadorID);
                    cmd.Parameters.AddWithValue("@Nombre", entrenador.Nombre);
                    cmd.Parameters.AddWithValue("@Especialidad", entrenador.Especialidad.ToString());
                    cmd.Parameters.AddWithValue("@Disponibilidad", entrenador.Disponibilidad);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void EliminarEntrenador (int entrenadorID)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Entrenadores WHERE EntrenadorID = @EntrenadorID", conexion))
                {
                    cmd.Parameters.AddWithValue("@EntrenadorID", entrenadorID);
                    int filasafectadas = cmd.ExecuteNonQuery();
                    if (filasafectadas == 0)
                    {
                        throw new Exception($"No se encontró un entrenador con el ID especificado {entrenadorID}");
                    }
                }
            }
        }
    }
}