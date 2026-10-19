using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class ActualizarCondicionJugadorRequest
{
    [Required]
    public bool? EsSuplente { get; set; }
}
