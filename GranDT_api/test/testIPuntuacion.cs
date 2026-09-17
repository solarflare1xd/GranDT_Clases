using GranDT_api.Models;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class PuntuacionRepositoryTests
    {
        [Fact]
        public void TestObtenerTodasPuntuaciones()
        {
            var repository = new PuntuacionRepository();
            var puntuaciones = repository.ObtenerTodos();

            Assert.NotNull(puntuaciones);
            Assert.IsType<List<Puntuacion>>(puntuaciones);
        }

        [Fact]
        public void TestAgregarPuntuacion()
        {
            var repository = new PuntuacionRepository();
            var puntuacion = new Puntuacion { IdPuntuacion = "PUNT001", Puntos = 10 };

            var resultado = repository.Agregar(puntuacion);

            Assert.NotNull(resultado);
            Assert.Equal(puntuacion.IdPuntuacion, resultado.IdPuntuacion);
            Assert.Equal(puntuacion.Puntos, resultado.Puntos);
        }

        [Fact]
        public void TestObtenerPuntuacionPorIdExistente()
        {
            var repository = new PuntuacionRepository();
            var puntuacion = new Puntuacion { IdPuntuacion = "PUNT001", Puntos = 10 };
            repository.Agregar(puntuacion);

            var resultado = repository.ObtenerPorId("PUNT001");

            Assert.NotNull(resultado);
            Assert.Equal("PUNT001", resultado.IdPuntuacion);
        }

        [Fact]
        public void TestObtenerPuntuacionPorIdInexistente()
        {
            var repository = new PuntuacionRepository();
            var resultado = repository.ObtenerPorId("PUNT999");

            Assert.Null(resultado);
        }

        [Fact]
        public void TestEliminarPuntuacionExistente()
        {
            var repository = new PuntuacionRepository();
            var puntuacion = new Puntuacion { IdPuntuacion = "PUNT001", Puntos = 10 };
            repository.Agregar(puntuacion);

            var eliminado = repository.Eliminar("PUNT001");

            Assert.True(eliminado);
            Assert.Null(repository.ObtenerPorId("PUNT001"));
        }

        [Fact]
        public void TestEliminarPuntuacionInexistente()
        {
            var repository = new PuntuacionRepository();
            var resultado = repository.Eliminar("PUNT999");

            Assert.False(resultado);
        }
    }
}