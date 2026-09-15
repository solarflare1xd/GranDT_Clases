using System.Collections.Generic;
using System.Linq;
using GranDT_Clases.IRepos;
namespace GranDT_Clases.Repositories;

public class JugadorRepositoryMemoria : IJugadorRepository
{
    private readonly List<Futbolista> jugadores = new();

    public List<Futbolista> ObtenerTodos()
    {
        return jugadores;
    }

    public Futbolista? ObtenerPorId(int id)
    {
        return jugadores.FirstOrDefault(j => j.IdJugador == id);
    }

    public Futbolista Agregar(Futbolista jugador)
    {
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