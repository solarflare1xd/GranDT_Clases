using GranDT_Clases;

namespace GranDT_api.Contracts.Responses;

public sealed class FutbolistaResponse
{
    public int IdJugador { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Apellido { get; init; } = string.Empty;
    public string? Apodo { get; init; }
    public decimal Precio { get; init; }
    public DateOnly FechaNacimiento { get; init; }
    public int? IdEquipo { get; init; }
    public EquipoResumenResponse? Equipo { get; init; }
    public Posicion? Posicion { get; init; }
    public List<Puntuacion> HistorialPuntajes { get; init; } = new();

    public static FutbolistaResponse Desde(Futbolista jugador, string? nombreEquipo)
    {
        return new FutbolistaResponse
        {
            IdJugador = jugador.IdJugador,
            Nombre = jugador.Nombre,
            Apellido = jugador.Apellido,
            Apodo = jugador.Apodo,
            Precio = jugador.Precio,
            FechaNacimiento = jugador.FechaNacimiento,
            IdEquipo = jugador.IdEquipo,
            Equipo = nombreEquipo == null ? null : new EquipoResumenResponse { Nombre = nombreEquipo },
            Posicion = jugador.Posicion,
            HistorialPuntajes = jugador.HistorialPuntajes
        };
    }
}

public sealed class EquipoResumenResponse
{
    public string Nombre { get; init; } = string.Empty;
}
