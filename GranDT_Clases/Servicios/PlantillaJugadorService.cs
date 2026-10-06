
using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Models;

namespace GranDT_Clases.Services;

public class PlantillaJugadorService
{
    private readonly IPlantillaJugadorRepository repository;
    private readonly IJugadorRepository? jugadorRepository;

    public PlantillaJugadorService(
        IPlantillaJugadorRepository repository,
        IJugadorRepository? jugadorRepository = null)
    {
        this.repository = repository;
        this.jugadorRepository = jugadorRepository;
    }

    public List<PlantillaJugador> ObtenerTodos() => repository.ObtenerTodos();

    public PlantillaJugador? ObtenerPorId(string idPlantilla, int idJugador) =>
        repository.ObtenerPorId(idPlantilla, idJugador);

    public PlantillaJugador Agregar(PlantillaJugador plantillaJugador) =>
        repository.Agregar(plantillaJugador);

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

        if (!MismaPosicion(idJugadorTitular, idJugadorSuplente))
        {
            throw new ArgumentException("El titular y el suplente deben tener la misma posición.");
        }

        return repository.IntercambiarTitularSuplente(idPlantilla, idJugadorTitular, idJugadorSuplente);
    }

    public bool Eliminar(string idPlantilla, int idJugador) =>
        repository.Eliminar(idPlantilla, idJugador);

    private bool MismaPosicion(int idJugadorTitular, int idJugadorSuplente)
    {
        var titular = jugadorRepository!.ObtenerPorId(idJugadorTitular);
        var suplente = jugadorRepository.ObtenerPorId(idJugadorSuplente);

        return titular?.Posicion != null
            && suplente?.Posicion != null
            && titular.Posicion.IdPosicion == suplente.Posicion.IdPosicion;
    }
}