using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Models;

namespace GranDT_api.Tests;

internal sealed class FakeEquipoRepository : IEquipoRepository
{
    private readonly Dictionary<string, Equipo> items = new();
    public List<Equipo> ObtenerTodos() => items.Values.ToList();
    public Equipo? ObtenerPorNombre(string nombre) => items.GetValueOrDefault(nombre);
    public Equipo Agregar(Equipo equipo) { items[equipo.Nombre] = equipo; return equipo; }
    public bool Eliminar(string nombre) => items.Remove(nombre);
}

internal sealed class FakeJugadorRepository : IJugadorRepository
{
    private readonly Dictionary<int, Futbolista> items = new();
    public List<Futbolista> ObtenerTodos() => items.Values.ToList();
    public Futbolista? ObtenerPorId(int id) => items.GetValueOrDefault(id);
    public Futbolista Agregar(Futbolista futbolista) { items[futbolista.IdJugador] = futbolista; return futbolista; }
    public bool Eliminar(int id) => items.Remove(id);
}

internal sealed class FakePlantillaRepository : IPlantillaRepository
{
    private readonly Dictionary<string, Plantilla> items = new();
    public List<Plantilla> ObtenerTodos() => items.Values.ToList();
    public Plantilla? ObtenerPorId(string id) => items.GetValueOrDefault(id);
    public Plantilla Agregar(Plantilla plantilla) { items[plantilla.IdPlantilla] = plantilla; return plantilla; }
    public bool Eliminar(string id) => items.Remove(id);
}

internal sealed class FakePlantillaJugadorRepository : IPlantillaJugadorRepository
{
    private readonly Dictionary<(string PlantillaId, int JugadorId), PlantillaJugador> items = new();
    public List<PlantillaJugador> ObtenerTodos() => items.Values.ToList();
    public PlantillaJugador? ObtenerPorId(string plantillaId, int jugadorId) => items.GetValueOrDefault((plantillaId, jugadorId));
    public PlantillaJugador Agregar(PlantillaJugador plantillaJugador) { items[(plantillaJugador.IdPlantilla, plantillaJugador.IdJugador)] = plantillaJugador; return plantillaJugador; }
    public bool Eliminar(string plantillaId, int jugadorId) => items.Remove((plantillaId, jugadorId));
}

internal sealed class FakePuntuacionRepository : IPuntuacionRepository
{
    private readonly Dictionary<string, Puntuacion> items = new();
    public int? UltimoJugadorId { get; private set; }
    public List<Puntuacion> ObtenerTodos() => items.Values.ToList();
    public Puntuacion? ObtenerPorId(string id) => items.GetValueOrDefault(id);
    public Puntuacion Agregar(Puntuacion puntuacion, int idJugador) { items[puntuacion.IdPuntuacion] = puntuacion; UltimoJugadorId = idJugador; return puntuacion; }
    public bool Eliminar(string id) => items.Remove(id);
}

internal sealed class FakeUsuarioRepository : IUsuarioRepository
{
    private readonly Dictionary<string, Usuario> items = new();
    public List<Usuario> ObtenerTodos() => items.Values.ToList();
    public Usuario? ObtenerPorEmail(string email) => items.GetValueOrDefault(email);
    public Usuario Agregar(Usuario usuario) { items[usuario.Email] = usuario; return usuario; }
    public bool Eliminar(string email) => items.Remove(email);
}

internal sealed class FakePosicionRepository : IPosicionRepository
{
    private readonly Dictionary<int, Posicion> items = new();
    public List<Posicion> ObtenerTodos() => items.Values.ToList();
    public Posicion? ObtenerPorId(int id) => items.GetValueOrDefault(id);
    public Posicion Agregar(Posicion posicion) { items[posicion.IdPosicion] = posicion; return posicion; }
    public bool Eliminar(int id) => items.Remove(id);
}