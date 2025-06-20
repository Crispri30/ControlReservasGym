namespace ControlGYM.Modelos
{
    public enum Especialidad
    {
        Cardiovascular,
        Musculación,
        Spinning,
        Yoga
    }
    public class Entrenadores
    {
        public int EntrenadorID { get; set; }
        public string Nombre { get; set; }
        public Especialidad Especialidad { get; set; }
        public bool Disponibilidad { get; set; } // Indica si el entrenador está disponible para asignar a un usuario

        public override string ToString() => Nombre; //Para mostrar el nombre del entrenador en listas o combos
    }
}
