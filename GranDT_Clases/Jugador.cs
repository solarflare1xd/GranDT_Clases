namespace GranDT_Clases
{
    public class Jugador
    {
        string Nombre { get; set; }
        string Apellido { get; set; }
        string Apodo { get; set; }
        DateOnly FechaNacimiento { get; set; } = new DateOnly();

        string Team { get; set; }
    }
}
