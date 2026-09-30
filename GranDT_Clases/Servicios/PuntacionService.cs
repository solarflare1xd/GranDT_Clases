
using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Repositories;

namespace GranDT_Clases.Services;

public class PuntuacionService
{
    private readonly IPuntuacionRepository repository;

    public PuntuacionService(IPuntuacionRepository repository)
    {
        this.repository = repository;
    }

    public List<Puntuacion> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Puntuacion? ObtenerPorId(string idPuntuacion)
    {
        return repository.ObtenerPorId(idPuntuacion);
    }

    public Puntuacion Agregar(Puntuacion puntuacion, int idJugador)
    {
        return repository.Agregar(puntuacion, idJugador);
    }

    public bool Eliminar(string idPuntuacion)
    {
        return repository.Eliminar(idPuntuacion);
    }
}