
using GranDT_Clases;
using GranDT_Clases.Servicios;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class JugadorServiceTests
    {
        [Fact]
        public void TestObtenerTodosJugadores()
        {
            var repository = new FakeJugadorRepository();
            var service = new JugadorService(repository);

            var jugadores = service.ObtenerTodos();

            Assert.NotNull(jugadores);
            Assert.Empty(jugadores);
        }

        [Fact]
        public void TestAgregarJugador()
        {
            var repository = new FakeJugadorRepository();
            var service = new JugadorService(repository);

            var jugador = new Futbolista
            {
                IdJugador = 1,
                Nombre = "Lionel",
                Apellido = "Messi",
                Apodo = "Leo",
                Precio = 100
            };

            var resultado = service.Agregar(jugador);

            Assert.NotNull(resultado);
            Assert.Equal(jugador.IdJugador, resultado.IdJugador);
            Assert.Equal(jugador.Nombre, resultado.Nombre);
            Assert.Equal(jugador.Apellido, resultado.Apellido);
        }

        [Fact]
        public void TestObtenerJugadorPorIdExistente()
        {
            var repository = new FakeJugadorRepository();
            var service = new JugadorService(repository);

            var jugador = new Futbolista
            {
                IdJugador = 1,
                Nombre = "Lionel",
                Apellido = "Messi"
            };

            service.Agregar(jugador);

            var resultado = service.ObtenerPorId(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdJugador);
        }

        [Fact]
        public void TestObtenerJugadorPorIdInexistente()
        {
            var repository = new FakeJugadorRepository();
            var service = new JugadorService(repository);

            var resultado = service.ObtenerPorId(999);

            Assert.Null(resultado);
        }

        [Fact]
        public void TestObtenerJugadoresPorNombreParcial()
        {
            var repository = new FakeJugadorRepository();
            var service = new JugadorService(repository);
            service.Agregar(new Futbolista { IdJugador = 1, Nombre = "Juan" });
            service.Agregar(new Futbolista { IdJugador = 2, Nombre = "Juan Pablo" });
            service.Agregar(new Futbolista { IdJugador = 3, Nombre = "Lionel" });

            var resultado = service.ObtenerPorNombre("juan");

            Assert.Equal(2, resultado.Count);
            Assert.All(resultado, jugador =>
                Assert.Contains("juan", jugador.Nombre, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void TestObtenerPorNombreRechazaNombreVacio()
        {
            var service = new JugadorService(new FakeJugadorRepository());

            Assert.Throws<ArgumentException>(() => service.ObtenerPorNombre(" "));
        }

        [Fact]
        public void TestEliminarJugadorExistente()
        {
            var repository = new FakeJugadorRepository();
            var service = new JugadorService(repository);

            var jugador = new Futbolista
            {
                IdJugador = 1,
                Nombre = "Lionel",
                Apellido = "Messi"
            };

            service.Agregar(jugador);

            var eliminado = service.Eliminar(1);

            Assert.True(eliminado);
            Assert.Null(service.ObtenerPorId(1));
        }

        [Fact]
        public void TestEliminarJugadorInexistente()
        {
            var repository = new FakeJugadorRepository();
            var service = new JugadorService(repository);

            var resultado = service.Eliminar(999);

            Assert.False(resultado);
        }
    }
}
