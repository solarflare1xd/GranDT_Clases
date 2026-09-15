using System.Collections.Generic;
using System.Linq;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories;

public class PuntuacionRepositoryMemoria : IPuntuacionRepository
{
    private readonly List<Puntuacion> Puntuaciones = new();

    public List<Puntuacion> ObtenerTodos()
    {
        return Puntuaciones;
    }

    public Puntuacion? ObtenerPorId(string id)
    {
        return Puntuaciones.FirstOrDefault(p => p.IdPuntuacion == id);
    }
    public Puntuacion Agregar(Puntuacion puntuacion)
    {
        Puntuaciones.Add(puntuacion);
        return puntuacion;
    }

    public bool Eliminar(string id)
    {
        var puntuacion = ObtenerPorId(id);

        if (puntuacion == null)
            return false;

        Puntuaciones.Remove(puntuacion);
        return true;
    }
}