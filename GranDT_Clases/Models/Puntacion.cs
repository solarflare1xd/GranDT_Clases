namespace GranDT_Clases;

public class Puntuacion
{
    public string IdPuntuacion { get; set; } = string.Empty;
    public int Fecha { get; set; }

    public decimal Puntaje { get; set; }
    public int IdJugador { get; set; }

    public DateOnly Partido_date { get; set; }
}
