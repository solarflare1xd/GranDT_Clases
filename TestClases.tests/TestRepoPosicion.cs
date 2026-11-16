namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoPosicion : TestRepo
{
    [Fact]
    public void ObtenerPosicionValidaOK()
    {
        var id = ObtenerIdPosicionValida();

        Assert.NotEqual(0, id);
    }

    [Fact]
    public void TraerPosicionesOK()
    {
        var id = ObtenerIdPosicionValida();
        var posiciones = Consultar<int>("sp_ObtenerTodasPosiciones");

        Assert.Contains(id, posiciones);
    }
}
