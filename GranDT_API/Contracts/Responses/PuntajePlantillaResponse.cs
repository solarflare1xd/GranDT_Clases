namespace GranDT_api.Contracts.Responses;

public sealed class PuntajePlantillaResponse
{
    public int IdPlantilla { get; init; }
    public int Fecha { get; init; }
    public decimal Puntaje { get; init; }
}
