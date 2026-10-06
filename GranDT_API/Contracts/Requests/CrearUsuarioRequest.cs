namespace GranDT_api.Contracts.Requests;

public sealed class CrearUsuarioRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly Nacimiento { get; set; }
    public string Password { get; set; } = string.Empty;
    public bool EsAdministrador { get; set; }
    public int? IdPlantilla { get; set; }
}
