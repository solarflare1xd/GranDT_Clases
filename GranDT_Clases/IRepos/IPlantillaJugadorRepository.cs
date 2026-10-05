using GranDT_Clases;

namespace GranDT_Clases.Models;

public interface IPlantillaJugadorRepository
{
    List<PlantillaJugador> ObtenerTodos();

    PlantillaJugador? ObtenerPorId(string idPlantilla, int idJugador);

    PlantillaJugador Agregar(PlantillaJugador plantillaJugador);

    bool IntercambiarTitularSuplente(string idPlantilla, int idJugadorTitular, int idJugadorSuplente)
    {
        throw new NotSupportedException("El repositorio no permite intercambiar titulares y suplentes.");
    }

    bool Eliminar(string idPlantilla, int idJugador);
}
