using ControlGYM.Modelos;
using ControlGYM.Utilidades;
using Microsoft.Data.SqlClient;
namespace ControlGYM.Repositorios
{
    public class UsuariosRepository
    {
        //Método para agregar un usuario a la base de datos

        public void AgregarUsuario(Usuarios usuario)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand verificar = new SqlCommand("SELECT COUNT (*) FROM Usuarios WHERE UsuarioID = @UsuarioID", conexion))
                {
                    verificar.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                    int cant = (int)verificar.ExecuteScalar();
                    if (cant > 0)
                    {
                        throw new Exception("El usuario ya existe en la base de datos.");
                    }
                }
                //En todos los casos si se crea un usuario, la fecha de vencimiento de la membresía se establece a un mes a partir de la fecha de creación.
                DateTime fechacreacion = DateTime.Now;
                DateTime fechavencimiento = fechacreacion.AddMonths(1);

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (UsuarioID,Nombre,Email,Telefono,FechaVencimientoMembresia,TipoMembresia) VALUES (@UsuarioID, @Nombre,@Email,@Telefono,@FechaVencimientoMembresia,@TipoMembresia))", conexion))
                {
                    cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@FechaVencimientoMembresia", fechavencimiento );
                    cmd.Parameters.AddWithValue("@TipoMembresia", usuario.TipoMembresia.ToString());

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Modelos.Usuarios> ObtenerUsuarios()
        {
            var lista_usuarios = new List<Modelos.Usuarios>();
            //Método para obtener todos los usuarios de la base de datos
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand ("SELECT UsuarioID, Nombre,Email,Telefono,FechaVencimientoMembresia,TipoMembresia FROM Usuarios", conexion))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_usuarios.Add(new Modelos.Usuarios
                            {
                                UsuarioID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Email = reader.GetString(2),
                                Telefono = reader.GetString(3),
                                FechaVencimientoMembresia = reader.GetDateTime(4),
                                TipoMembresia = (TipoMembresia)Enum.Parse(typeof(TipoMembresia), reader.GetString(5))
                            });
                        }
                    }
                }
                return lista_usuarios;
            }
        }
        public void ActualizarDatosUsuario (Usuarios usuario)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand ("UPDATE Usuarios SET Nombre = @Nombre, Email = @Email, Telefono = @Telefono, TipoMembresia = @TipoMembresia WHERE UsuarioID = @UsuarioID", conexion))
                {
                    cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@TipoMembresia", usuario.TipoMembresia.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ActualizarMembresiaUsuario (Usuarios usuario)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET FechaVencimientoMembresia = @FechaMembresia", conexion))
                {
                    DateTime fecha_actual = DateTime.Now;
                    DateTime fecha_vencimiento =  fecha_actual.AddMonths(1);
                    cmd.Parameters.AddWithValue("@FechaMembresia", fecha_vencimiento);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarUsuario(int usuarioID)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerInstancia().CrearConexion())
            {
                using (SqlCommand cmd =  new SqlCommand("DELETE Usuarios WHERE UsuarioID = @UsuarioID", conexion))
                {
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    int filas_afectadas = cmd.ExecuteNonQuery();
                    if (filas_afectadas == 0)
                    {
                        throw new Exception("No se encontró el usuario con el ID especificado.");
                    }
                }
            }
        }
    }
}
