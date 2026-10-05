namespace GranDT_api.Contracts.Requests;

public sealed class AgregarJugadorAPlantillaRequest
{
    public string IdPlantilla { get; set; } = string.Empty;
    public int IdJugador { get; set; }
    public int Numero { get; set; }
    public bool EsSuplente { get; set; }
}
