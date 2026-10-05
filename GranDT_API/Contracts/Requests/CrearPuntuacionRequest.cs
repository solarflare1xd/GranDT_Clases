namespace GranDT_api.Contracts.Requests;

public sealed class CrearPuntuacionRequest
{
    public string IdPuntuacion { get; set; } = string.Empty;
    public int Fecha { get; set; }
    public decimal Puntaje { get; set; }
    public int IdJugador { get; set; }
}
