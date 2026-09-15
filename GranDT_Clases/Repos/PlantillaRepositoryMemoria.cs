using System.Collections.Generic;
using System.Linq;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories;

public class PlantillaRepositoryMemoria : IPlantillaRepository
{
    private readonly List<Plantilla> Plantillas = new();

    public List<Plantilla> ObtenerTodos()
    {
        return Plantillas;
    }

    public Plantilla? ObtenerPorId(string id)
    {
        return Plantillas.FirstOrDefault(p => p.IdPlantilla == id);
    }
    public Plantilla Agregar(Plantilla plantilla)
    {
        Plantillas.Add(plantilla);
        return plantilla;
    }

    public bool Eliminar(string id)
    {
        var plantilla = ObtenerPorId(id);

        if (plantilla == null)
            return false;

        Plantillas.Remove(plantilla);
        return true;
    }
}