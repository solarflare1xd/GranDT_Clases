

namespace GranDT_api.Models;

public interface IEquipoRepository
{
    List<Equipo> ObtenerTodos();
    Equipo? ObtenerPorId(string nombre);
    Equipo Agregar(Equipo equipo);
    bool Eliminar(string nombre);
}