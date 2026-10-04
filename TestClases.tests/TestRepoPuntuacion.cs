namespace GranDT_api.Tests;

[Trait("Category", "Integration")]
public class TestRepoPuntuacion : TestRepo
{
    [Fact]
    public void AltaPuntuacionOK()
    {
        var idJugador = CrearJugador(CrearPosicion());
        var idPuntuacion = $"IT-{Guid.NewGuid():N}";
        Ejecutar(
            "INSERT INTO Puntuacion (IdPuntuacion, Partido_date, Puntaje, IdJugador) VALUES (@idPuntuacion, @fecha, @puntaje, @idJugador)",
            new
            {
                idPuntuacion,
                fecha = new DateTime(2026, 9, 28),
                puntaje = 8,
                idJugador
            },
            System.Data.CommandType.Text);

        Assert.Equal(8, ConsultarUno<int>(
            "SELECT Puntaje FROM Puntuacion WHERE IdPuntuacion = @idPuntuacion",
            new { idPuntuacion },
            System.Data.CommandType.Text));
    }

    [Fact]
    public void EliminarJugadorEliminaSusPuntuaciones()
    {
        var idJugador = CrearJugador(CrearPosicion());
        var idPuntuacion = $"IT-{Guid.NewGuid():N}";
        Ejecutar(
            "INSERT INTO Puntuacion (IdPuntuacion, Partido_date, Puntaje, IdJugador) VALUES (@idPuntuacion, @fecha, @puntaje, @idJugador)",
            new
            {
                idPuntuacion,
                fecha = new DateTime(2026, 9, 28),
                puntaje = 8,
                idJugador
            },
            System.Data.CommandType.Text);

        Ejecutar("sp_EliminarJugador", new { p_IdJugador = idJugador });

        Assert.Equal(0, ConsultarUno<int>(
            "SELECT COUNT(*) FROM Puntuacion WHERE IdPuntuacion = @idPuntuacion",
            new { idPuntuacion },
            System.Data.CommandType.Text));
    }
}
