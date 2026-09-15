using System.Collections.Generic;
using System.Linq;
using GranDT_Clases.IRepos;
namespace GranDT_Clases.Repositories;

public class EquipoRepositoryMemoria : IEquipoRepository
{
    private readonly List<Equipo> equipos = new();

    public List<Equipo> ObtenerTodos()
    {
        return equipos;
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

    public bool Eliminar(string nombre)
    {
        var equipo = ObtenerPorNombre(nombre);

        if (equipo == null)
            return false;

        equipos.Remove(equipo);

        return true;
    }
}