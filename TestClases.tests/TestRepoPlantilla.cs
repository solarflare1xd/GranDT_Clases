namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoPlantilla : TestRepo
{
    [Fact]
    public void AltaPlantillaOK()
    {
        var id = CrearPlantilla();

        Assert.Equal(id, ConsultarUno<int>(
            "sp_ObtenerPlantillaPorId",
            new { p_IdPlantilla = id }));
    }

    [Fact]
    public void TraerPlantillasOK()
    {
        var id = CrearPlantilla();

        Assert.Contains(id, Consultar<int>("sp_ObtenerTodasPlantillas"));
    }

    private int CrearPlantilla()
    {
        return ConsultarUno<int>("sp_AgregarPlantilla", new { p_Presupuesto = 100m });
    }
}
