namespace GranDT_Clases.IRepos;

public interface IPuntuacionRepository
{
    List<Puntuacion> ObtenerTodos();
    Puntuacion? ObtenerPorId(string IdPuntuacion);
    Puntuacion Agregar(Puntuacion puntuacion);
    bool Eliminar(string IdPuntuacion);
}