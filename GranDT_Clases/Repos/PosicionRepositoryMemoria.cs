using System.Collections.Generic;
using System.Linq;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories;

public class PosicionRepositoryMemoria : IPosicionRepository
{
    private readonly List<Posicion> posiciones = new();

    public List<Posicion> ObtenerTodos()
    {
        return posiciones;
    }

    public Posicion? ObtenerPorId(int id)
    {
        return posiciones.FirstOrDefault(p => p.IdPosicion == id);
    }

    public Posicion Agregar(Posicion posicion)
    {
        posiciones.Add(posicion);

        return posicion;
    }

    public bool Eliminar(int id)
    {
        var posicion = ObtenerPorId(id);

        if (posicion == null)
            return false;

        posiciones.Remove(posicion);

        return true;
    }
}