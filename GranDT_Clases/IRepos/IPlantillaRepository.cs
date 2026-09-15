namespace GranDT_Clases.IRepos;

public interface IPlantillaRepository
{
    List<Plantilla> ObtenerTodos();
    Plantilla? ObtenerPorNombre(string IdPlantilla);
    Plantilla Agregar(Plantilla plantilla);
    bool Eliminar(string IdPlantilla);
}