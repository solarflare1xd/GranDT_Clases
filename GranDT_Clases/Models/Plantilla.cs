namespace GranDT_Clases;

public class Plantilla
{
    public int IdPlantilla { get; set; }
    public decimal Presupuesto { get; set; }
    public int CantidadMaximaJugadores { get; set; }
    public List<Futbolista> Jugadores { get; set; } = new();
    public List<Futbolista> Jugadores_sup { get; set; } = new();
}
