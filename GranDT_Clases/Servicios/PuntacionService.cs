
using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Repositories;
using GranDT_Clases.Servicios;

namespace GranDT_Clases.Services;

public class PuntuacionService
{
    private readonly IPuntuacionRepository repository;
    private readonly JugadorService? jugadorService;

    public PuntuacionService(IPuntuacionRepository repository, JugadorService? jugadorService = null)
    {
        this.repository = repository;
        this.jugadorService = jugadorService;
    }

    public List<Puntuacion> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Puntuacion? ObtenerPorId(string idPuntuacion)
    {
        return repository.ObtenerPorId(idPuntuacion);
    }

    public Puntuacion Agregar(Puntuacion puntuacion, int idJugador)
    {
        return repository.Agregar(puntuacion, idJugador);
    }

    public Puntuacion? Agregar(string idPuntuacion, int fecha, decimal puntaje, int idJugador)
    {
        if (string.IsNullOrWhiteSpace(idPuntuacion) || fecha is < 1 or > 49 || puntaje is < 1 or > 10)
            throw new ArgumentException("La puntuación, fecha e identificador no son válidos.");

        if (jugadorService?.ObtenerPorId(idJugador) == null)
        {
            if (jugadorService == null)
                throw new InvalidOperationException("No está configurada la validación del futbolista.");

            return null;
        }

        var puntuacion = new Puntuacion
        {
            IdPuntuacion = idPuntuacion.Trim(),
            Fecha = fecha,
            Puntaje = puntaje,
            IdJugador = idJugador
        };

        return repository.Agregar(puntuacion, idJugador);
    }

    public bool Eliminar(string idPuntuacion)
    {
        return repository.Eliminar(idPuntuacion);
    }
}