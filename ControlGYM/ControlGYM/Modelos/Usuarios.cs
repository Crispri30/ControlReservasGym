using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ControlGYM.Modelos
{
    public enum TipoMembresia
    {
        Normal,
        Premium
    }
    public class Usuarios
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaVencimientoMembresia { get; set; }
        public TipoMembresia TipoMembresia { get; set; }
    }
}
