namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoPosicion : TestRepo
{
    [Fact]
    public void AltaPosicionOK()
    {
        var nombre = $"IT{Guid.NewGuid():N}"[..30];
        var id = ConsultarUno<int>("sp_AgregarPosicion", new { p_Nombre = nombre });

        var posicion = ConsultarUno<PosicionResultado>(
            "sp_ObtenerPosicionPorId",
            new { p_IdPosicion = id });

        Assert.Equal(id, posicion.IdPosicion);
        Assert.Equal(nombre, posicion.Nombre);
    }

    [Fact]
    public void TraerPosicionesOK()
    {
        var id = CrearPosicion();

        Assert.Contains(id, Consultar<int>("sp_ObtenerTodasPosiciones"));
    }

    private sealed class PosicionResultado
    {
        public int IdPosicion { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
