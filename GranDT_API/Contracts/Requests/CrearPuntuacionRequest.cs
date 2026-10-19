using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class CrearPuntuacionRequest
{
    [Required, StringLength(50, MinimumLength = 1)]
    public string IdPuntuacion { get; set; } = string.Empty;
    [Range(1, 49)]
    public int Fecha { get; set; }
    [Range(typeof(decimal), "1", "10")]
    public decimal Puntaje { get; set; }
    public int IdJugador { get; set; }
}
