using Microsoft.Data.SqlClient;

namespace ControlGYM.Utilidades
{
    public class ConexionBD
    {
        //Creación de string de conexión a la base de datos
        private readonly string connectionstring;

        //Creacion instancia de la clase ConexionBD (Singleton)
        private static ConexionBD? instancia;

        //Bloqueo para evitar problemas de concurrencia
        private static readonly object lockObj = new object();

        private ConexionBD()
        {
            //Agregar la cadena de conexión a la variable connectionstring
            connectionstring = "Server=localhost\\SQLEXPRESS;Database=ControlGym;Trusted_Connection=True;TrustServerCertificate=True";
        }
        //Método para obtener la instancia de la clase ConexionBD
        public static ConexionBD ObtenerInstancia()
        {
            if (instancia == null)
            {
                lock (lockObj) // Bloqueo para evitar problemas de concurrencia
                {
                    if (instancia == null) //Verificar si la instancia es nula nuevamente
                    {
                        instancia = new ConexionBD(); //asignar una nueva instancia
                    }
                }
            }
            return instancia; //Devolver la instancia
        }

        public SqlConnection CrearConexion()
        {
            var conexion = new SqlConnection(connectionstring); //Crear y devolver una nueva conexión
            conexion.Open(); //Abrir la conexión
            return conexion;
        }
    }
}