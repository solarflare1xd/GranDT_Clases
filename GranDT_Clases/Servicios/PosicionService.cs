using GranDT_Clases.IRepos;

namespace GranDT_Clases.Servicios;

public class PosicionService
{
    private readonly IPosicionRepository repository;

    public PosicionService(IPosicionRepository repository)
    {
        this.repository = repository;
    }

    public List<Posicion> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Posicion? ObtenerPorId(int id)
    {
        return repository.ObtenerPorId(id);
    }

    public Posicion Agregar(Posicion posicion)
    {
        return repository.Agregar(posicion);
    }

    public bool Eliminar(int id)
    {
        return repository.Eliminar(id);
    }
}