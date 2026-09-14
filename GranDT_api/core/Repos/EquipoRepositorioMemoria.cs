using System.Collections.Generic;
using System.Linq;
using GranDT_Clases;
using GranDT_api.Models;

namespace GranDT_api.Repositories;

public class EquipoRepositoryMemoria : IEquipoRepository
{
    private readonly List<Equipo> equipos = new();

    public List<Equipo> ObtenerTodos()
    {
        return equipos;
    }

    public Equipo? ObtenerPorId(string nombre)
    {
        return equipos.FirstOrDefault(e => e.Nombre == nombre);
    }

    public Equipo Agregar(Equipo equipo)
    {
        // Como el identificador es el nombre (string), no autoincrementamos IDs numéricos. 
        // Simplemente validamos que no exista o lo agregamos directamente a la lista.
        equipos.Add(equipo);

        return equipo;
    }

    public bool Eliminar(string nombre)
    {
        var equipo = ObtenerPorId(nombre);

        if (equipo == null)
            return false;

        equipos.Remove(equipo);

        return true;
    }
}