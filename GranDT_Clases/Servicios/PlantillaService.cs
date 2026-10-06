using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Services;

public class PlantillaService
{
    private const decimal PresupuestoMaximoPermitido = 9999999999999999.99m;
    private const int CantidadMaximaPermitida = 20;
    private static readonly (string Posicion, int Maximo)[] LimitesPorPosicion =
    {
        ("Arquero", 1),
        ("Defensor", 4),
        ("Mediocampista", 2),
        ("Delantero", 4)
    };

    private readonly IPlantillaRepository repository;
    private readonly IJugadorRepository? jugadorRepository;

    public PlantillaService(IPlantillaRepository repository, IJugadorRepository? jugadorRepository = null)
    {
        this.repository = repository;
        this.jugadorRepository = jugadorRepository;
    }

    public List<Plantilla> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Plantilla? ObtenerPorId(int id)
    {
        return repository.ObtenerPorId(id);
    }

    public Plantilla? ObtenerPorUsuario(string email)
    {
        return repository.ObtenerPorUsuario(email);
    }

    public Plantilla Agregar(Plantilla plantilla)
    {
        return repository.Agregar(plantilla);
    }

    public decimal ObtenerGasto(Plantilla plantilla)
    {
        return plantilla.Jugadores
            .Concat(plantilla.Jugadores_sup)
            .Sum(jugador => jugador.Precio);
    }

    public decimal ObtenerPresupuestoDisponible(Plantilla plantilla)
    {
        return plantilla.Presupuesto - ObtenerGasto(plantilla);
    }

    public Plantilla CrearCompleta(
        decimal presupuesto,
        IReadOnlyCollection<PlantillaJugador> integrantes)
    {
        if (jugadorRepository == null)
        {
            throw new InvalidOperationException("Server Internal Error");
        }

        if (presupuesto <= 0 || presupuesto > PresupuestoMaximoPermitido)
        {
            throw new ArgumentOutOfRangeException(
                nameof(presupuesto),
                $"El presupuesto debe ser mayor que 0 y no superar {PresupuestoMaximoPermitido}.");
        }

        if (integrantes.Select(integrante => integrante.IdJugador).Distinct().Count() != integrantes.Count)
        {
            throw new ArgumentException("No se puede agregar dos veces al mismo jugador.", nameof(integrantes));
        }

        var plantilla = new Plantilla
        {
            Presupuesto = presupuesto,
            CantidadMaximaJugadores = CantidadMaximaPermitida
        };

        foreach (var integrante in integrantes)
        {
            var futbolista = jugadorRepository.ObtenerPorId(integrante.IdJugador)
                ?? throw new ArgumentException($"No existe el jugador {integrante.IdJugador}.", nameof(integrantes));

            integrante.Futbolista = futbolista;

            if (integrante.EsSuplente)
            {
                plantilla.Jugadores_sup.Add(futbolista);
            }
            else
            {
                plantilla.Jugadores.Add(futbolista);
            }
        }

        ValidarPlantilla(plantilla, integrantes.Count);

        return repository.CrearCompleta(plantilla, integrantes);
    }

    public decimal PuntajeFecha(Plantilla plantilla, int fecha)
    {
        if (fecha is < 1 or >= 50)
        {
            throw new ArgumentOutOfRangeException(nameof(fecha), "La fecha debe estar entre 1 y 49.");
        }

        return plantilla.Jugadores
            .SelectMany(jugador => jugador.HistorialPuntajes)
            .Where(puntuacion => puntuacion.Fecha == fecha)
            .Sum(puntuacion => puntuacion.Puntaje);
    }

    private void ValidarPlantilla(Plantilla plantilla, int cantidadIntegrantes)
    {
        var gasto = ObtenerGasto(plantilla);
        var errores = new List<string>();

        if (gasto > plantilla.Presupuesto)
        {
            errores.Add("el costo total supera el presupuesto disponible");
        }

        if (cantidadIntegrantes > plantilla.CantidadMaximaJugadores)
        {
            errores.Add($"la cantidad supera el máximo de {plantilla.CantidadMaximaJugadores} jugadores");
        }

        if (!TieneFormacionTitularDentroDeLimites(plantilla.Jugadores))
        {
            errores.Add("la plantilla no puede superar 1 arquero, 4 defensores, 4 mediocampistas y  2 delanteros titulares");
        }

        if (errores.Count > 0)
        {
            throw new ArgumentException($"La plantilla no es válida: {string.Join("; ", errores)}.", nameof(plantilla));
        }
    }

    private static bool TieneFormacionTitularDentroDeLimites(List<Futbolista> titulares)
    {
        return LimitesPorPosicion.All(limite =>
            titulares.Count(jugador => jugador.Posicion?.Nombre == limite.Posicion) <= limite.Maximo);
    }

    public bool Eliminar(int id)
    {
        return repository.Eliminar(id);
    }
}