namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoPlantillaJugador : TestRepo
{
    [Fact]
    public void AltaPlantillaJugadorOK()
    {
        var idPlantilla = $"IT-{Guid.NewGuid():N}";
        var idJugador = CrearJugador(CrearPosicion());
        Ejecutar("sp_AgregarPlantilla", new { p_IdPlantilla = idPlantilla, p_Presupuesto = 100m });
        Ejecutar("sp_AgregarPlantillaJugador", new
        {
            p_IdPlantilla = idPlantilla,
            p_IdJugador = idJugador,
            p_Numero = 10,
            p_EsSuplente = false
        });

        var cantidad = ConsultarUno<int>(
            "SELECT COUNT(*) FROM PlantillaJugador WHERE IdPlantilla = @idPlantilla AND IdJugador = @idJugador",
            new { idPlantilla, idJugador },
            System.Data.CommandType.Text);

        Assert.Equal(1, cantidad);
    }

    [Fact]
    public void TraerPlantillaJugadoresOK()
    {
        var idPlantilla = $"IT-{Guid.NewGuid():N}";
        var idJugador = CrearJugador(CrearPosicion());
        Ejecutar("sp_AgregarPlantilla", new { p_IdPlantilla = idPlantilla, p_Presupuesto = 100m });
        Ejecutar("sp_AgregarPlantillaJugador", new
        {
            p_IdPlantilla = idPlantilla,
            p_IdJugador = idJugador,
            p_Numero = 10,
            p_EsSuplente = false
        });

        Assert.Contains(idPlantilla, Consultar<string>("sp_ObtenerTodasPlantillaJugadores"));
    }
}
