
using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Models;

namespace GranDT_Clases.Repositories;

public class PlantillaJugadorRepositoryMemoria : IPlantillaJugadorRepository
{
    private readonly List<PlantillaJugador> plantillaJugadores = new();

    public List<PlantillaJugador> ObtenerTodos()
    {
        return plantillaJugadores;
    }

    public PlantillaJugador? ObtenerPorId(string idPlantilla, int idJugador)
    {
        return plantillaJugadores.FirstOrDefault(pj =>
            pj.IdPlantilla == idPlantilla &&
            pj.IdJugador == idJugador);
    }

    public PlantillaJugador Agregar(PlantillaJugador plantillaJugador)
    {
        plantillaJugadores.Add(plantillaJugador);

        return plantillaJugador;
    }

    public bool Eliminar(string idPlantilla, int idJugador)
    {
        var plantillaJugador = ObtenerPorId(idPlantilla, idJugador);

        if (plantillaJugador == null)
            return false;

        plantillaJugadores.Remove(plantillaJugador);

        return true;
    }
}
