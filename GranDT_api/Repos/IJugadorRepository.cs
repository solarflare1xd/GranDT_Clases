

namespace GranDT_api.Models;

public interface IJugadorRepository
{
    List<Jugador> ObtenerTodos();
    Jugador? ObtenerPorId(int id);
    Jugador Agregar(Jugador jugador);
    bool Eliminar(int id);
}