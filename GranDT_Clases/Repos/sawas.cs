using System.Collections.Generic;
using System.Linq;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories;

public class PlantillaRepositoryMemoria : IPlantillaRepository
{
    private readonly List<Equipo> equipos = new();

    public List<Equipo> ObtenerTodos()
    {
        return equipos;
    }

    public Equipo? ObtenerPorfecha(int id)
    {
        return equipos.FirstOrDefault(e => e.IdEquipo == id);
    }

    public Equipo? ObtenerPorNombre(string nombre)
    {
        return equipos.FirstOrDefault(e => e.Nombre == nombre);
    }

    public Equipo Agregar(Equipo equipo)
    {
        equipos.Add(equipo);
        return equipo;
    }

    public bool Eliminar(int id)
    {
        var equipo = ObtenerPorId(id);

        if (equipo == null)
            return false;

        equipos.Remove(equipo);
        return true;
    }
}