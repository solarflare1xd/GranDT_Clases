using GranDT_Clases.IRepos;



namespace GranDT_Clases.Servicios;


public class JugadorService
{
    private readonly IJugadorRepository repository;

    public JugadorService(IJugadorRepository repository)
    {
        this.repository = repository;
    }

    public List<Futbolista> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Futbolista? ObtenerPorId(int id)
    {
        return repository.ObtenerPorId(id);
    }

    public Futbolista Agregar(Futbolista futbolista)
    {
        return repository.Agregar(futbolista);
    }

    public bool Eliminar(int id)
    {
        return repository.Eliminar(id);
    }
}