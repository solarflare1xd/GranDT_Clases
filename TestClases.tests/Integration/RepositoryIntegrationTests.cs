using GranDT_Clases.Repositories;
using Xunit;

namespace GranDT_api.Tests.Integration;

[Trait("Category", "Integration")]
public class RepositoryIntegrationTests
{
    [DatabaseFact]
    public void Equipo_ObtenerTodos_SeConectaALaBase()
    {
        Assert.NotNull(new EquipoRepositoryMemoria().ObtenerTodos());
    }

    [DatabaseFact]
    public void Futbolista_ObtenerTodos_SeConectaALaBase()
    {
        Assert.NotNull(new JugadorRepositoryMemoria().ObtenerTodos());
    }

    [DatabaseFact]
    public void Plantilla_ObtenerTodos_SeConectaALaBase()
    {
        Assert.NotNull(new PlantillaRepositoryMemoria().ObtenerTodos());
    }

    [DatabaseFact]
    public void PlantillaJugador_ObtenerTodos_SeConectaALaBase()
    {
        Assert.NotNull(new PlantillaJugadorRepositoryMemoria().ObtenerTodos());
    }

    [DatabaseFact]
    public void Posicion_ObtenerTodos_SeConectaALaBase()
    {
        Assert.NotNull(new PosicionRepositoryMemoria().ObtenerTodos());
    }

    [DatabaseFact]
    public void Puntuacion_ObtenerTodos_SeConectaALaBase()
    {
        Assert.NotNull(new PuntuacionRepositoryMemoria().ObtenerTodos());
    }

    [DatabaseFact]
    public void Usuario_ObtenerTodos_SeConectaALaBase()
    {
        Assert.NotNull(new UsuarioRepositoryMemoria().ObtenerTodos());
    }
}

public sealed class DatabaseFactAttribute : FactAttribute
{
    public DatabaseFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("GRANDT_TEST_CONNECTION_STRING")))
        {
            Skip = "Define GRANDT_TEST_CONNECTION_STRING para ejecutar pruebas de integración.";
        }
    }
}