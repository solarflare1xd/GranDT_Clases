using GranDT_Clases;
using GranDT_api.Repositories;
using GranDT_api.Services;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class PuntuacionServiceTests
    {
        [Fact]
        public void TestObtenerTodasPuntuaciones()
        {
            var repository = new PuntuacionRepository();
            var service = new PuntuacionService(repository);

            var puntuaciones = service.ObtenerTodos();

            Assert.NotNull(puntuaciones);
            Assert.IsType<List<Puntuacion>>(puntuaciones);
        }

        [Fact]
        public void TestAgregarPuntuacion()
        {
            var repository = new PuntuacionRepository();
            var service = new PuntuacionService(repository);

            var puntuacion = new Puntuacion
            {
                IdPuntuacion = "PUNT001",
                Partido_date = new DateOnly(2026, 9, 28),
                Puntaje = 10
            };

            var resultado = service.Agregar(puntuacion);

            Assert.NotNull(resultado);
            Assert.Equal(puntuacion.IdPuntuacion, resultado.IdPuntuacion);
            Assert.Equal(puntuacion.Puntaje, resultado.Puntaje);
        }

        [Fact]
        public void TestObtenerPuntuacionPorIdExistente()
        {
            var repository = new PuntuacionRepository();
            var service = new PuntuacionService(repository);

            var puntuacion = new Puntuacion
            {
                IdPuntuacion = "PUNT001",
                Partido_date = new DateOnly(2026, 9, 28),
                Puntaje = 10
            };

            service.Agregar(puntuacion);

            var resultado = service.ObtenerPorId("PUNT001");

            Assert.NotNull(resultado);
            Assert.Equal("PUNT001", resultado.IdPuntuacion);
        }

        [Fact]
        public void TestObtenerPuntuacionPorIdInexistente()
        {
            var repository = new PuntuacionRepository();
            var service = new PuntuacionService(repository);

            var resultado = service.ObtenerPorId("PUNT999");

            Assert.Null(resultado);
        }

        [Fact]
        public void TestEliminarPuntuacionExistente()
        {
            var repository = new PuntuacionRepository();
            var service = new PuntuacionService(repository);

            var puntuacion = new Puntuacion
            {
                IdPuntuacion = "PUNT001",
                Partido_date = new DateOnly(2026, 9, 28),
                Puntaje = 10
            };

            service.Agregar(puntuacion);

            var eliminado = service.Eliminar("PUNT001");

            Assert.True(eliminado);
            Assert.Null(service.ObtenerPorId("PUNT001"));
        }

        [Fact]
        public void TestEliminarPuntuacionInexistente()
        {
            var repository = new PuntuacionRepository();
            var service = new PuntuacionService(repository);

            var resultado = service.Eliminar("PUNT999");

            Assert.False(resultado);
        }
    }
}
