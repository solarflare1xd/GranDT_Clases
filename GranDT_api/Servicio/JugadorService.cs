using BibliotecaApi.Models;
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Services;


public class JugadorService
{
    private readonly IJugadorRepository repository;

    public JugadorService(IJugadorRepository repository)
    {
        this.repository = repository;
    }

    public List<Jugador> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Jugador? ObtenerPorId(int id)
    {
        return repository.ObtenerPorId(id);
    }

    public Jugador Agregar(Jugador jugador)
    {
        return repository.Agregar(jugador);
    }

    public bool Eliminar(int id)
    {
        return repository.Eliminar(id);
    }
}
