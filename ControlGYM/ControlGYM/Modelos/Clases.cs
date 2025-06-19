namespace ControlGYM.Modelos
{
    public class Clases
    {
        public string Nombre { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public int Duracion { get; set; } // Duración en minutos
        public int CapacidadMaxima { get; set; }
        public int EntrenadorID { get; set; } // ID del entrenador asignado a la clase
    }
}
