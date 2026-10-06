using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Models;

namespace GranDT_api.Tests;

internal sealed class FakeEquipoRepository : IEquipoRepository
{
    private readonly Dictionary<string, Equipo> equipos = new();

    public List<Equipo> ObtenerTodos()
    {
        return equipos.Values.ToList();
    }

    public Equipo? ObtenerPorNombre(string nombre)
    {
        return equipos.GetValueOrDefault(nombre);
    }

    public Equipo Agregar(Equipo equipo)
    {
        equipos[equipo.Nombre] = equipo;
        return equipo;
    }

    public bool Eliminar(string nombre)
    {
        return equipos.Remove(nombre);
    }
}

internal sealed class FakeJugadorRepository : IJugadorRepository
{
    private readonly Dictionary<int, Futbolista> jugadores = new();

    public List<Futbolista> ObtenerTodos()
    {
        return jugadores.Values.ToList();
    }

    public Futbolista? ObtenerPorId(int id)
    {
        return jugadores.GetValueOrDefault(id);
    }

    public List<Futbolista> ObtenerPorNombre(string nombre)
    {
        return jugadores.Values
            .Where(jugador => jugador.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Futbolista Agregar(Futbolista futbolista)
    {
        jugadores[futbolista.IdJugador] = futbolista;
        return futbolista;
    }

    public bool Eliminar(int id)
    {
        return jugadores.Remove(id);
    }
}

internal sealed class FakePlantillaRepository : IPlantillaRepository
{
    private readonly Dictionary<int, Plantilla> plantillas = new();

    public List<Plantilla> ObtenerTodos()
    {
        return plantillas.Values.ToList();
    }

    public Plantilla? ObtenerPorId(int id)
    {
        return plantillas.GetValueOrDefault(id);
    }

    public Plantilla Agregar(Plantilla plantilla)
    {
        plantillas[plantilla.IdPlantilla] = plantilla;
        return plantilla;
    }

    public Plantilla CrearCompleta(Plantilla plantilla, IReadOnlyCollection<PlantillaJugador> integrantes)
    {
        plantilla.IdPlantilla = plantillas.Count + 1;
        plantillas[plantilla.IdPlantilla] = plantilla;
        return plantilla;
    }

    public bool Eliminar(int id)
    {
        return plantillas.Remove(id);
    }
}

internal sealed class FakePlantillaJugadorRepository : IPlantillaJugadorRepository
{
    private readonly Dictionary<(int PlantillaId, int JugadorId), PlantillaJugador> integrantes = new();

    public List<PlantillaJugador> ObtenerTodos()
    {
        return integrantes.Values.ToList();
    }

    public PlantillaJugador? ObtenerPorId(int plantillaId, int jugadorId)
    {
        return integrantes.GetValueOrDefault((plantillaId, jugadorId));
    }

    public PlantillaJugador Agregar(PlantillaJugador plantillaJugador)
    {
        var clave = (plantillaJugador.IdPlantilla, plantillaJugador.IdJugador);
        integrantes[clave] = plantillaJugador;
        return plantillaJugador;
    }

    public bool Eliminar(int plantillaId, int jugadorId)
    {
        return integrantes.Remove((plantillaId, jugadorId));
    }
}

internal sealed class FakePuntuacionRepository : IPuntuacionRepository
{
    private readonly Dictionary<string, Puntuacion> puntuaciones = new();

    public int? UltimoJugadorId { get; private set; }

    public List<Puntuacion> ObtenerTodos()
    {
        return puntuaciones.Values.ToList();
    }

    public Puntuacion? ObtenerPorId(string id)
    {
        return puntuaciones.GetValueOrDefault(id);
    }

    public Puntuacion Agregar(Puntuacion puntuacion, int idJugador)
    {
        puntuaciones[puntuacion.IdPuntuacion] = puntuacion;
        UltimoJugadorId = idJugador;
        return puntuacion;
    }

    public bool Eliminar(string id)
    {
        return puntuaciones.Remove(id);
    }
}

internal sealed class FakeUsuarioRepository : IUsuarioRepository
{
    private readonly Dictionary<string, Usuario> usuarios = new();

    public List<Usuario> ObtenerTodos()
    {
        return usuarios.Values.ToList();
    }

    public Usuario? ObtenerPorEmail(string email)
    {
        return usuarios.GetValueOrDefault(email);
    }

    public Usuario Agregar(Usuario usuario)
    {
        usuarios[usuario.Email] = usuario;
        return usuario;
    }

    public bool Eliminar(string email)
    {
        return usuarios.Remove(email);
    }
}

internal sealed class FakePosicionRepository : IPosicionRepository
{
    private readonly Dictionary<int, Posicion> posiciones = new();

    public List<Posicion> ObtenerTodos()
    {
        return posiciones.Values.ToList();
    }

    public Posicion? ObtenerPorId(int id)
    {
        return posiciones.GetValueOrDefault(id);
    }

    public Posicion Agregar(Posicion posicion)
    {
        posiciones[posicion.IdPosicion] = posicion;
        return posicion;
    }

    public bool Eliminar(int id)
    {
        return posiciones.Remove(id);
    }
}
