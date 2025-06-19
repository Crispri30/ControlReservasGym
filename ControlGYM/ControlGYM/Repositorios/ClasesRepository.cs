using Microsoft.Data.SqlClient;
using ControlGYM.Modelos;
using ControlGYM.Utilidades;

namespace ControlGYM.Repositorios
{
    public class ClasesRepository
    {
        public void AgregarClase(Clases clase)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Clases (Nombre, FechaHoraInicio, FechaHoraFin, Duracion, CapacidadMaxima, EntrenadorID) VALUES (@Nombre, @FechaHoraInicio, @FechaHoraFin, @Duracion, @CapacidadMaxima, @EntrenadorID)", conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", clase.Nombre);
                    cmd.Parameters.AddWithValue("@FechaHoraInicio", clase.FechaHoraInicio);
                    cmd.Parameters.AddWithValue("@FechaHoraFin", clase.FechaHoraFin);
                    cmd.Parameters.AddWithValue("@Duracion", clase.Duracion);
                    cmd.Parameters.AddWithValue("@CapacidadMaxima", clase.CapacidadMaxima);
                    cmd.Parameters.AddWithValue("@EntrenadorID", clase.EntrenadorID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Clases> ObtenerClases()
        {
            var lista_clases = new List<Clases>();
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT  NombreClase, FechaHoraInicio, FechaHoraFin, Duracion, CapacidadMaxima, EntrenadorID FROM Clases", conexion))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_clases.Add(new Clases
                            {
                                ClaseID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                FechaHoraInicio = reader.GetDateTime(2),
                                FechaHoraFin = reader.GetDateTime(3),
                                Duracion = reader.GetInt32(4),
                                CapacidadMaxima = reader.GetInt32(5),
                                EntrenadorID = reader.GetInt32(6)
                            });
                        }
                    }
                }
                return lista_clases;
            }
        }

        public void ActualizarClase(Clases clase)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Clases SET NombreClase = @Nombre, FechaHoraInicio =@FechaHoraInicio, FechaHoraFin = @FechaHoraFin, Duracion = @Duracion, CapacidadMaxima = @CapacidadMaxima WHERE ClaseID = @ClaseID", conexion))
                {
                    cmd.Parameters.AddWithValue("@ClaseID", clase.ClaseID);
                    cmd.Parameters.AddWithValue("@Nombre", clase.Nombre);
                    cmd.Parameters.AddWithValue("@FechaHoraInicio", clase.FechaHoraInicio);
                    cmd.Parameters.AddWithValue("@FechaHoraFin", clase.FechaHoraFin);
                    cmd.Parameters.AddWithValue("@Duracion", clase.Duracion);
                    cmd.Parameters.AddWithValue("@CapacidadMaxima", clase.CapacidadMaxima);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarClase(int claseID)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand ("DELETE FROM Clases WHERE ClaseID = @ClaseID"))
                {
                    cmd.Parameters.AddWithValue("@ClaseID", claseID);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas == 0)
                    {
                        throw new Exception($"No se encontro una clase con el ID especificado {claseID}");
                    }
                }
            }
        }
    }
}