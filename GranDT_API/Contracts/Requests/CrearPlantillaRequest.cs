using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class CrearPlantillaRequest
{
    [Range(typeof(decimal), "0.01", "9999999999999999.99")]
    public decimal Presupuesto { get; set; }
    public List<int>? Titulares { get; set; } = new();
    public List<int>? Suplentes { get; set; } = new();
}
