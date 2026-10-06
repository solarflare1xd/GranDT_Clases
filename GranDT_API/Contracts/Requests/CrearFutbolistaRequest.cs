using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class CrearFutbolistaRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Apodo { get; set; }
    [Range(typeof(decimal), "0", "99999999.99")]
    public decimal Precio { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public int IdPosicion { get; set; }
    public int? IdEquipo { get; set; }
}
