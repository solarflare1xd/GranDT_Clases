namespace GranDT_api.Contracts.Responses;

public sealed class ValidacionPlantillaResponse
{
    public int IdPlantilla { get; init; }
    public bool EsValida { get; init; }
    public List<string> Errores { get; init; } = new();
}
