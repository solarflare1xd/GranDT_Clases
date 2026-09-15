using GranDT_Clases;

namespace GranDT_Clases.Models;

public interface IPlantillaJugadorRepository
{
    List<PlantillaJugador> ObtenerTodos();

    PlantillaJugador? ObtenerPorId(string idPlantilla, int idJugador);

    PlantillaJugador Agregar(PlantillaJugador plantillaJugador);

    bool Eliminar(string idPlantilla, int idJugador);
}
