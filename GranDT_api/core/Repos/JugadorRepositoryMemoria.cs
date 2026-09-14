using BibliotecaApi.Models; // <-- Corregido (en plural y con el nombre del proyecto correcto)
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Repositories;

public class JugadorRepositoryMemoria : IJugadorRepository
{
    private readonly List<Jugador> jugadores = new();

    public List<Jugador> ObtenerTodos()
    {
        return jugadores;
    }

    public Jugador? ObtenerPorId(int id)
    {
        return jugadores.FirstOrDefault(j => j.Id == id);
    }

    public Jugador Agregar(Jugador jugador)
    {
        jugador.Id = jugadores.Count > 0
            ? jugadores.Max(j => j.Id) + 1
            : 1;

        jugadores.Add(jugador);

        return jugador;
    }

    public bool Eliminar(int id)
    {
        var jugador = ObtenerPorId(id);

        if (jugador == null)
            return false;

        jugadores.Remove(jugador);

        return true;
    }
}