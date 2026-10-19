using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class CrearUsuarioRequest
{
    [Required, StringLength(50, MinimumLength = 1)]
    public string Nombre { get; set; } = string.Empty;
    [Required, StringLength(50, MinimumLength = 1)]
    public string Apellido { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = string.Empty;
    public DateOnly Nacimiento { get; set; }
    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
