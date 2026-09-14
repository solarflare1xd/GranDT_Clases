namespace GranDT_Clases
{
    public class Futbolista
    {
        string Nombre { get; set; }
        string Apellido { get; set; }
        string Apodo { get; set; }

        float Precio { get; set; }
        DateOnly FechaNacimiento { get; set; } = new DateOnly();

        string Posicion { get; set; }

        DateOnly partido_date { get; set; } = new DateOnly();
        int puntaje { get; set; }

    }
}
