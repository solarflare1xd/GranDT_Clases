namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoEquipo : TestRepo
{
    [Fact]
    public void AltaEquipoOK()
    {
        var nombre = CrearEquipo();

        Assert.Equal(nombre, ConsultarUno<string>(
            "sp_ObtenerEquipoPorNombre",
            new { p_Nombre = nombre }));
    }

    [Fact]
    public void TraerEquiposOK()
    {
        var nombre = CrearEquipo();

        Assert.Contains(nombre, Consultar<string>("sp_ObtenerTodosEquipos"));
    }

    private string CrearEquipo()
    {
        var nombre = $"Equipo {NuevoIdPrueba()}";
        Ejecutar("sp_AgregarEquipo", new { p_Nombre = nombre });
        return nombre;
    }
}
