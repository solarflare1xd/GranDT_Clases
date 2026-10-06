using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class CrearPlantillaRequest
{
    [Range(typeof(decimal), "0.01", "99999999.99")]
    public decimal Presupuesto { get; set; }
    public List<IntegrantePlantillaRequest> Jugadores { get; set; } = new();
}
