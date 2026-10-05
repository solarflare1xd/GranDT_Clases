namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoPosicion : TestRepo
{
    [Fact]
    public void AltaPosicionOK()
    {
        var id = CrearPosicion();

        Assert.NotEqual(0, id);
    }

    [Fact]
    public void TraerPosicionesOK()
    {
        var id = CrearPosicion();
        var posiciones = Consultar<int>("sp_ObtenerTodasPosiciones");

        Assert.Contains(id, posiciones);
    }
}
