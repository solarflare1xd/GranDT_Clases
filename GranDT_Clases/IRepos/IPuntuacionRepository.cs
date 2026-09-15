namespace GranDT_Clases.IRepos;

public interface IPuntuacionRepository
{
    List<Puntacion> ObtenerTodos();
    Puntacion? ObtenerPorId(string IdPuntacion);
    Puntacion Agregar(Puntacion puntuacion);
    bool Eliminar(string IdPuntacion);
}