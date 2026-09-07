namespace GranDT_Clases
{
    public class Futbolista
    {
        string Nombre { get; set; }
        string Apellido { get; set; }
        string Apodo { get; set; }

        float precio { get; set; }
        DateOnly FechaNacimiento { get; set; } = new DateOnly();

        Equipo Team { get; set; }

        string Posicion { get; set; }
    }
}
