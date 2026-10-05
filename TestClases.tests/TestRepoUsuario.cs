namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoUsuario : TestRepo
{
    [Fact]
    public void AltaUsuarioOK()
    {
        var email = CrearUsuario();

        Assert.Equal(email, ConsultarUno<string>(
            "sp_ObtenerUsuarioPorEmail",
            new { p_Email = email }));
    }

    [Fact]
    public void TraerUsuariosOK()
    {
        var email = CrearUsuario();

        Assert.Contains(email, Consultar<string>("sp_ObtenerTodosUsuarios"));
    }

    private string CrearUsuario()
    {
        const string email = "integration-test@example.test";
        Ejecutar("sp_AgregarUsuario", new
        {
            p_Email = email,
            p_Nombre = "Integration",
            p_Apellido = "Test",
            p_Nacimiento = new DateTime(2000, 1, 1),
            p_Password = "integration-test-hash",
            p_EsAdministrador = false,
            p_IdPlantilla = (string?)null
        });
        return email;
    }
}
