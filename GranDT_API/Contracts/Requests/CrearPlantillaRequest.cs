namespace GranDT_api.Contracts.Requests;

public sealed class CrearPlantillaRequest
{
    public string IdPlantilla { get; set; } = string.Empty;
    public List<IntegrantePlantillaRequest> Jugadores { get; set; } = new();
}
