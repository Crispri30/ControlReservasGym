namespace ControlGYM.Modelos
{
    public enum EstadoReserva
    {
        Activa = 1,
        Cancelada = 0
    }
    public class Reserva
    {
        public int ReservaID { get; set; }
        public int UsuarioID { get; set; }
        public int ClaseID { get; set; }
        public DateTime FechaReserva { get; set; } // Fecha y hora en que se realizó la reserva
        public EstadoReserva Estado { get; set; }
        public DateTime? FechaCancelacion { get; set; }

    }
}
