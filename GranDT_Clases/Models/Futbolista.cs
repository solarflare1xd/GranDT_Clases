namespace GranDT_Clases
{
    public class Futbolista
    {
        public int IdJugador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Apodo { get; set; }

        public float Precio { get; set; }
        public DateOnly FechaNacimiento { get; set; } = new DateOnly();

        public string Posicion { get; set; }

        public List<Puntuacion> HistorialPuntajes { get; set; } = new List<Puntuacion>();

    }
}
