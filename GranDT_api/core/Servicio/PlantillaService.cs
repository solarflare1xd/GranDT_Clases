using GranDT_Clases;
using GranDT_Clases.Repositories;

namespace GranDT_Clases.Services;

public class PlantillaService
{
    private readonly IPlantillaRepository repository;

    public PlantillaService(IPlantillaRepository repository)
    {
        this.repository = repository;
    }

    public List<Plantilla> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Plantilla? ObtenerPorId(string id)
    {
        return repository.ObtenerPorId(id);
    }

    public Plantilla Agregar(Plantilla plantilla)
    {
        return repository.Agregar(plantilla);
    }

    public bool Eliminar(string id)
    {
        return repository.Eliminar(id);
    }
}