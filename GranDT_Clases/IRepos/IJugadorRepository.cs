namespace GranDT_Clases.IRepos;

public interface IJugadorRepository
{
    List<Futbolista> ObtenerTodos();
    Futbolista? ObtenerPorId(int id);
    List<Futbolista> ObtenerPorNombre(string nombre);
    Futbolista Agregar(Futbolista jugador);
    Futbolista? Actualizar(Futbolista jugador) => throw new NotSupportedException("El repositorio no permite actualizar futbolistas.");
    bool Eliminar(int id);
}