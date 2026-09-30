namespace GranDT_Clases.IRepos;

public interface IPuntuacionRepository
{
    List<Puntuacion> ObtenerTodos();
    Puntuacion? ObtenerPorId(string IdPuntuacion);
    bool Eliminar(string IdPuntuacion);
}