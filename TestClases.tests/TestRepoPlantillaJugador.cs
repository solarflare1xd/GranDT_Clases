namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoPlantillaJugador : TestRepo
{
    [Fact]
    public void AltaPlantillaJugadorOK()
    {
        var (idPlantilla, idJugador) = CrearPlantillaJugador();

        var cantidad = ConsultarUno<int>(
            "SELECT COUNT(*) FROM PlantillaJugador WHERE IdPlantilla = @idPlantilla AND IdJugador = @idJugador",
            new { idPlantilla, idJugador },
            System.Data.CommandType.Text);

        Assert.Equal(1, cantidad);
    }

    [Fact]
    public void TraerPlantillaJugadoresOK()
    {
        var (idPlantilla, _) = CrearPlantillaJugador();

        Assert.Contains(idPlantilla, Consultar<int>("sp_ObtenerTodasPlantillaJugadores"));
    }

    private (int IdPlantilla, int IdJugador) CrearPlantillaJugador()
    {
        var idPlantilla = ConsultarUno<int>("sp_AgregarPlantilla", new { p_Presupuesto = 100m });
        var idJugador = CrearJugador(CrearPosicion());
        Ejecutar("sp_AgregarPlantillaJugador", new
        {
            p_IdPlantilla = idPlantilla,
            p_IdJugador = idJugador,
            p_Numero = 10,
            p_EsSuplente = false
        });
        return (idPlantilla, idJugador);
    }
}
