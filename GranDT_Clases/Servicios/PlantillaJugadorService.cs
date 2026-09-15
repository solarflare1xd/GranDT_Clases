
using GranDT_Clases;
using GranDT_Clases.Models;
using GranDT_Clases.Repositories;

namespace GranDT_Clases.Services;

public class PlantillaJugadorService
{
    private readonly IPlantillaJugadorRepository repository;

    public PlantillaJugadorService(IPlantillaJugadorRepository repository)
    {
        this.repository = repository;
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

    public bool Eliminar(string idPlantilla, int idJugador)
    {
        return repository.Eliminar(idPlantilla, idJugador);
    }
}