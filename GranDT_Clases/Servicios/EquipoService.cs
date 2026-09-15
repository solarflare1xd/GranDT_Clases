using System.Collections.Generic;
using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Servicios;

public class EquipoService
{
    private readonly IEquipoRepository repository;

    public EquipoService(IEquipoRepository repository)
    {
        this.repository = repository;
    }

    public List<Equipo> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Equipo? ObtenerPorNombre(string nombre)
    {
        return repository.ObtenerPorNombre(nombre);
    }

    public Equipo Agregar(Equipo equipo)
    {
        return repository.Agregar(equipo);
    }

    public bool Eliminar(string nombre)
    {
        return repository.Eliminar(nombre);
    }
}