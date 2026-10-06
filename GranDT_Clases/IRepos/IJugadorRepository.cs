namespace GranDT_Clases.IRepos;

public interface IJugadorRepository
{
    List<Futbolista> ObtenerTodos();
    Futbolista? ObtenerPorId(int id);
    List<Futbolista> ObtenerPorNombre(string nombre);
    Futbolista Agregar(Futbolista jugador);
    bool Eliminar(int id);
}