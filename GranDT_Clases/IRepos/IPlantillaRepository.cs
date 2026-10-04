namespace GranDT_Clases.IRepos;

public interface IPlantillaRepository
{
    List<Plantilla> ObtenerTodos();
    Plantilla? ObtenerPorId(string IdPlantilla);
    Plantilla Agregar(Plantilla plantilla);
    bool Eliminar(string IdPlantilla);
    Plantilla? ObtenerPorUsuario(string email) => null;
    ResultadoValidacionPlantilla? Validar(string idPlantilla) => null;
}