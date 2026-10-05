
using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Models;

namespace GranDT_Clases.Services;

public class PlantillaJugadorService
{
    private readonly IPlantillaJugadorRepository repository;
    private readonly IJugadorRepository? jugadorRepository;

    public PlantillaJugadorService(IPlantillaJugadorRepository repository)
    {
        this.repository = repository;
    }

    public PlantillaJugadorService(
        IPlantillaJugadorRepository repository,
        IJugadorRepository jugadorRepository)
        : this(repository)
    {
        this.jugadorRepository = jugadorRepository;
    }

    public List<PlantillaJugador> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public PlantillaJugador? ObtenerPorId(string idPlantilla, int idJugador)
    {
        return repository.ObtenerPorId(idPlantilla, idJugador);
    }

    public PlantillaJugador Agregar(PlantillaJugador plantillaJugador)
    {
        return repository.Agregar(plantillaJugador);
    }

    public bool IntercambiarTitularSuplente(string idPlantilla, int idJugadorTitular, int idJugadorSuplente)
    {
        if (jugadorRepository == null)
        {
            throw new InvalidOperationException("No está configurada la validación de la plantilla.");
        }

        if (idJugadorTitular == idJugadorSuplente)
        {
            throw new ArgumentException("Se deben indicar dos jugadores distintos.", nameof(idJugadorSuplente));
        }

        var titular = repository.ObtenerPorId(idPlantilla, idJugadorTitular);
        var suplente = repository.ObtenerPorId(idPlantilla, idJugadorSuplente);
        if (titular == null || suplente == null)
        {
            return false;
        }

        if (titular.EsSuplente || !suplente.EsSuplente)
        {
            throw new ArgumentException("El primer jugador debe ser titular y el segundo debe ser suplente.");
        }

        var futbolistaTitular = jugadorRepository.ObtenerPorId(idJugadorTitular)
            ?? throw new InvalidOperationException($"No existe el jugador {idJugadorTitular} asociado a la plantilla.");
        var futbolistaSuplente = jugadorRepository.ObtenerPorId(idJugadorSuplente)
            ?? throw new InvalidOperationException($"No existe el jugador {idJugadorSuplente} asociado a la plantilla.");

        if (futbolistaTitular.Posicion == null
            || futbolistaSuplente.Posicion == null
            || futbolistaTitular.Posicion.IdPosicion != futbolistaSuplente.Posicion.IdPosicion)
        {
            throw new ArgumentException("El titular y el suplente deben tener la misma posición.");
        }

        return repository.IntercambiarTitularSuplente(idPlantilla, idJugadorTitular, idJugadorSuplente);
    }

    public bool Eliminar(string idPlantilla, int idJugador)
    {
        return repository.Eliminar(idPlantilla, idJugador);
    }

}