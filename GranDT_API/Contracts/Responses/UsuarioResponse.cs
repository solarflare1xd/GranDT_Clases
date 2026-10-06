using GranDT_Clases;

namespace GranDT_api.Contracts.Responses;

public sealed class UsuarioResponse
{
    public string Nombre { get; init; } = string.Empty;
    public string Apellido { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateOnly Nacimiento { get; init; }
    public bool EsAdministrador { get; init; }
    public int? IdPlantilla { get; init; }

    public static UsuarioResponse Desde(Usuario usuario)
    {
        return new UsuarioResponse
        {
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Email = usuario.Email,
            Nacimiento = usuario.Nacimiento,
            EsAdministrador = usuario.EsAdministrador,
            IdPlantilla = usuario.PlantillaUsuario?.IdPlantilla
        };
    }
}
