
using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Models;
using GranDT_Clases.Servicios;
using GranDT_Clases.Services;

namespace GranDT_Clases.Services;

public class PlantillaJugadorService
{
    private readonly IPlantillaJugadorRepository repository;
    private readonly IJugadorRepository? jugadorRepository;
    private readonly PlantillaService? plantillaService;

    public PlantillaJugadorService(
        IPlantillaJugadorRepository repository,
        IJugadorRepository? jugadorRepository = null,
        PlantillaService? plantillaService = null)
    {
        this.repository = repository;
        this.jugadorRepository = jugadorRepository;
        this.plantillaService = plantillaService;
    }

    public List<PlantillaJugador> ObtenerTodos() => repository.ObtenerTodos();

    public PlantillaJugador? ObtenerPorId(int idPlantilla, int idJugador) =>
        repository.ObtenerPorId(idPlantilla, idJugador);

    public PlantillaJugador Agregar(PlantillaJugador plantillaJugador) =>
        repository.Agregar(plantillaJugador);

    public PlantillaJugador Agregar(
        int idPlantilla,
        int idJugador,
        int numero,
        bool esSuplente)
    {
        if (idPlantilla <= 0)
            throw new ArgumentException("El identificador de plantilla debe ser mayor que cero.");

        return repository.Agregar(new PlantillaJugador
        {
            IdPlantilla = idPlantilla,
            IdJugador = idJugador,
            Numero = numero,
            EsSuplente = esSuplente
        });
    }

    public Plantilla? AgregarAPlantilla(
        int idPlantilla,
        int idJugador,
        int numero,
        bool esSuplente)
    {
        if (plantillaService == null || jugadorRepository == null)
            throw new InvalidOperationException("No está configurada la validación de la plantilla.");

        var plantilla = plantillaService.ObtenerCompletaPorId(idPlantilla);
        var futbolista = jugadorRepository.ObtenerPorId(idJugador);
        if (plantilla == null || futbolista == null)
            return null;

        if (repository.ObtenerPorId(idPlantilla, idJugador) != null)
            throw new ResourceConflictException("El futbolista ya pertenece a la plantilla.");

        plantillaService.ValidarAgregarJugador(plantilla, futbolista, esSuplente);
        repository.Agregar(new PlantillaJugador
        {
            IdPlantilla = idPlantilla,
            IdJugador = idJugador,
            Numero = numero,
            EsSuplente = esSuplente
        });

        return plantillaService.ObtenerCompletaPorId(idPlantilla);
    }

    public PlantillaJugador? ActualizarCondicion(int idPlantilla, int idJugador, bool esSuplente)
    {
        var integrante = repository.ObtenerPorId(idPlantilla, idJugador);
        if (integrante == null)
            return null;

        if (integrante.EsSuplente == esSuplente)
            throw new ArgumentException("El futbolista ya tiene la condición solicitada.", nameof(esSuplente));

        if (jugadorRepository == null)
            throw new InvalidOperationException("No está configurada la validación de la plantilla.");

        var futbolista = jugadorRepository.ObtenerPorId(idJugador)
            ?? throw new KeyNotFoundException($"No existe el futbolista {idJugador}.");
        integrante.Futbolista = futbolista;
        return repository.ActualizarCondicion(idPlantilla, idJugador, esSuplente);
    }

    public bool IntercambiarTitularSuplente(int idPlantilla, int idJugadorTitular, int idJugadorSuplente)
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

    public bool Eliminar(int idPlantilla, int idJugador) =>
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