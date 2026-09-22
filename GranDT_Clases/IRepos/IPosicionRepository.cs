namespace GranDT_Clases.IRepos;

public interface IPosicionRepository
{
    List<Posicion> ObtenerTodos();
    Posicion? ObtenerPorId(int id);
    Posicion Agregar(Posicion posicion);
    bool Eliminar(int id);
}