using GranDT_Clases;
using GranDT_Clases.Servicios;
using Xunit;

namespace GranDT_api.Tests;

public class PosicionServiceTests
{
    [Fact]
    public void ObtenerTodos_DevuelvePosiciones()
    {
        var repository = new FakePosicionRepository();
        var service = new PosicionService(repository);
        service.Agregar(new Posicion { IdPosicion = 1, Nombre = "Arquero" });

        var posiciones = service.ObtenerTodos();

        Assert.Single(posiciones);
        Assert.Equal("Arquero", posiciones[0].Nombre);
    }

    [Fact]
    public void ObtenerPorId_DevuelvePosicionExistente()
    {
        var repository = new FakePosicionRepository();
        var service = new PosicionService(repository);
        service.Agregar(new Posicion { IdPosicion = 1, Nombre = "Arquero" });

        var posicion = service.ObtenerPorId(1);

        Assert.NotNull(posicion);
        Assert.Equal("Arquero", posicion.Nombre);
    }

    [Fact]
    public void ObtenerPorId_DevuelveNullSiNoExiste()
    {
        var service = new PosicionService(new FakePosicionRepository());

        Assert.Null(service.ObtenerPorId(999));
    }

    [Fact]
    public void Eliminar_DevuelveFalseSiNoExiste()
    {
        var service = new PosicionService(new FakePosicionRepository());

        Assert.False(service.Eliminar(999));
    }
}