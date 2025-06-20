using Microsoft.Data.SqlClient;
using ControlGYM.Modelos;
using ControlGYM.Utilidades;
using Microsoft.AspNetCore.Identity;

namespace ControlGYM.Repositorios
{
    public class ReservasRepository
    {
        public void AgregarReserva (Reserva reserva)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RegistrarReserva", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UsuarioID", reserva.UsuarioID);
                        cmd.Parameters.AddWithValue("@ClaseID", reserva.ClaseID);
                        cmd.Parameters.AddWithValue("@Estado", ((byte)reserva.Estado));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al agregar la reserva: " + ex.Message);
                }
            }
        }

        public List<Reserva> ObtenerReservas()
        {
            var lista_reservas = new List<Reserva>();
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand ("SELECT ReservaID, UsuarioID, ClaseID, FechaReserva, Estado, FechaCancelacion FROM Reservas ", conexion))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_reservas.Add(new Reserva
                            {
                                ReservaID = reader.GetInt32(0),
                                UsuarioID = reader.GetInt32(1),
                                ClaseID = reader.GetInt32(2),
                                FechaReserva = reader.GetDateTime(3),
                                Estado = (EstadoReserva)reader.GetByte(4),
                                FechaCancelacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                            });
                        }
                    }
                }
                return lista_reservas;
            }
        }

        public void CancelarReserva(int reservaID, int usuarioID, int claseID)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_CancelarReserva", conexion))
                    {
                        cmd.Parameters.AddWithValue("@ReservaID", reservaID);
                        cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                        cmd.Parameters.AddWithValue("@ClaseID", claseID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al cancelar la reserva: " + ex.Message);
                }
            }
        }
    }
}
