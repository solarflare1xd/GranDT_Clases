namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoJugador : TestRepo
{
    [Fact]
    public void AltaJugadorOK()
    {
        var idPosicion = CrearPosicion();
        var idJugador = CrearJugador(idPosicion);

        Assert.Equal(idJugador, ConsultarUno<int>(
            "sp_ObtenerJugadorPorId",
            new { p_IdJugador = idJugador }));
    }

    [Fact]
    public void TraerJugadoresOK()
    {
        var idJugador = CrearJugador(CrearPosicion());

        Assert.Contains(idJugador, Consultar<int>("sp_ObtenerTodosJugadores"));
    }
}
