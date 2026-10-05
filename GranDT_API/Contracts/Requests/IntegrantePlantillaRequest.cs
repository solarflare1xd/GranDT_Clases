namespace GranDT_api.Contracts.Requests;

public sealed class IntegrantePlantillaRequest
{
    public int IdJugador { get; set; }
    public int Numero { get; set; }
    public bool EsSuplente { get; set; }
}
