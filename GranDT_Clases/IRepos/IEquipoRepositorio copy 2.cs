namespace GranDT_Clases.IRepos;

public interface IJugadorRepository
{
    List<Futbolista> ObtenerTodos();
    Futbolista? ObtenerPorId(int id);
    Futbolista Agregar(Futbolista jugador);
    bool Eliminar(int id);
}