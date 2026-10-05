namespace GranDT_api.Contracts.Requests;

public sealed class IntercambiarTitularSuplenteRequest
{
    public int IdJugadorTitular { get; set; }
    public int IdJugadorSuplente { get; set; }
}
