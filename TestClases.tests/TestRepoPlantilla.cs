namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoPlantilla : TestRepo
{
    [Fact]
    public void AltaPlantillaOK()
    {
        var id = CrearPlantilla();

        Assert.Equal(id, ConsultarUno<string>(
            "sp_ObtenerPlantillaPorId",
            new { p_IdPlantilla = id }));
    }

    [Fact]
    public void TraerPlantillasOK()
    {
        var id = CrearPlantilla();

        Assert.Contains(id, Consultar<string>("sp_ObtenerTodasPlantillas"));
    }

    private string CrearPlantilla()
    {
        var id = NuevoIdPrueba();
        Ejecutar("sp_AgregarPlantilla", new { p_IdPlantilla = id, p_Presupuesto = 100m });
        return id;
    }
}
