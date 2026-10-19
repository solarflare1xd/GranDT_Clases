using GranDT_Clases;

namespace GranDT_Clases.Models;

public interface IPlantillaJugadorRepository
{
    List<PlantillaJugador> ObtenerTodos();

    PlantillaJugador? ObtenerPorId(int idPlantilla, int idJugador);

    PlantillaJugador Agregar(PlantillaJugador plantillaJugador);
    PlantillaJugador? ActualizarCondicion(int idPlantilla, int idJugador, bool esSuplente) =>
        throw new NotSupportedException("El repositorio no permite actualizar la condición del jugador.");

    bool IntercambiarTitularSuplente(int idPlantilla, int idJugadorTitular, int idJugadorSuplente)
    {
        throw new NotSupportedException("El repositorio no permite intercambiar titulares y suplentes.");
    }

    bool Eliminar(int idPlantilla, int idJugador);
}
