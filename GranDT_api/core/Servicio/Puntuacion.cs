
using GranDT_Clases;
using GranDT_Clases.Repositories;

namespace GranDT_Clases.Services;

public class PuntacionService
{
    private readonly IPuntacionRepository repository;

    public PuntacionService(IPuntacionRepository repository)
    {
        this.repository = repository;
    }

    public List<Puntacion> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Puntacion? ObtenerPorId(string idPuntacion)
    {
        return repository.ObtenerPorId(idPuntacion);
    }

    public Puntacion Agregar(Puntacion puntacion)
    {
        return repository.Agregar(puntacion);
    }

    public bool Eliminar(string idPuntacion)
    {
        return repository.Eliminar(idPuntacion);
    }
}

