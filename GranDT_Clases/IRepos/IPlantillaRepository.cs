namespace GranDT_Clases.IRepos;

public interface IPlantillaRepository
{
    List<Plantilla> ObtenerTodos();
    Plantilla? ObtenerPorId(int idPlantilla);
    Plantilla Agregar(Plantilla plantilla);
    bool Eliminar(int idPlantilla);
    Plantilla? ObtenerPorUsuario(string email) => null;
    Plantilla CrearCompleta(Plantilla plantilla, IReadOnlyCollection<PlantillaJugador> integrantes)
    {
        throw new NotSupportedException("El repositorio no permite crear plantillas completas.");
    }
}