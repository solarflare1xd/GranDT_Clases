namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoEquipo : TestRepo
{
    [Fact]
    public void AltaEquipoOK()
    {
        var nombre = $"IT Equipo {Guid.NewGuid():N}";
        Ejecutar("sp_AgregarEquipo", new { p_Nombre = nombre });

        Assert.Equal(nombre, ConsultarUno<string>(
            "sp_ObtenerEquipoPorNombre",
            new { p_Nombre = nombre }));
    }

    [Fact]
    public void TraerEquiposOK()
    {
        var nombre = $"IT Equipo {Guid.NewGuid():N}";
        Ejecutar("sp_AgregarEquipo", new { p_Nombre = nombre });

        Assert.Contains(nombre, Consultar<string>("sp_ObtenerTodosEquipos"));
    }
}
