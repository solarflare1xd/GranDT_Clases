using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class CrearFutbolistaRequest
{
    [Required, StringLength(50, MinimumLength = 1)]
    public string Nombre { get; set; } = string.Empty;
    [Required, StringLength(50, MinimumLength = 1)]
    public string Apellido { get; set; } = string.Empty;
    [StringLength(50)]
    public string? Apodo { get; set; }
    [Range(typeof(decimal), "0.01", "99999999.99")]
    public decimal Precio { get; set; }
    [Required]
    public DateOnly FechaNacimiento { get; set; }
    [Range(1, int.MaxValue)]
    public int IdPosicion { get; set; }
    [Range(1, int.MaxValue)]
    public int? IdEquipo { get; set; }
}
