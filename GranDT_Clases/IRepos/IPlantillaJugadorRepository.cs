using GranDT_Clases;

namespace GranDT_Clases.Models;

public interface IPlantillaJugadorRepository
{
    List<PlantillaJugador> ObtenerTodos();

    PlantillaJugador? ObtenerPorId(int idPlantilla, int idJugador);

    PlantillaJugador Agregar(PlantillaJugador plantillaJugador);

    bool IntercambiarTitularSuplente(int idPlantilla, int idJugadorTitular, int idJugadorSuplente)
    {
        throw new NotSupportedException("El repositorio no permite intercambiar titulares y suplentes.");
    }

    bool Eliminar(int idPlantilla, int idJugador);
}
