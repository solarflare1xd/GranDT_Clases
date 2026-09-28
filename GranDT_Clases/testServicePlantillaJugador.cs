using GranDT_Clases;
using GranDT_Clases.Models;
using GranDT_Clases.Services;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class PlantillaJugadorServiceTests
    {
        [Fact]
        public void TestObtenerTodosPlantillaJugadores()
        {
            var repository = new PlantillaJugadorRepository();
            var service = new PlantillaJugadorService(repository);

            var plantillaJugadores = service.ObtenerTodos();

            Assert.NotNull(plantillaJugadores);
            Assert.IsType<List<PlantillaJugador>>(plantillaJugadores);
        }

        [Fact]
        public void TestAgregarPlantillaJugador()
        {
            var repository = new PlantillaJugadorRepository();
            var service = new PlantillaJugadorService(repository);

            var plantillaJugador = new PlantillaJugador
            {
                IdPlantilla = "PLA001",
                IdJugador = 1,
                Numero = 10,
                EsSuplente = false
            };

            var resultado = service.Agregar(plantillaJugador);

            Assert.NotNull(resultado);
            Assert.Equal(plantillaJugador.IdPlantilla, resultado.IdPlantilla);
            Assert.Equal(plantillaJugador.IdJugador, resultado.IdJugador);
            Assert.Equal(plantillaJugador.Numero, resultado.Numero);
            Assert.Equal(plantillaJugador.EsSuplente, resultado.EsSuplente);
        }

        [Fact]
        public void TestObtenerPlantillaJugadorPorIdExistente()
        {
            var repository = new PlantillaJugadorRepository();
            var service = new PlantillaJugadorService(repository);

            var plantillaJugador = new PlantillaJugador
            {
                IdPlantilla = "PLA001",
                IdJugador = 1,
                Numero = 10,
                EsSuplente = false
            };

            service.Agregar(plantillaJugador);

            var resultado = service.ObtenerPorId("PLA001", 1);

            Assert.NotNull(resultado);
            Assert.Equal("PLA001", resultado.IdPlantilla);
            Assert.Equal(1, resultado.IdJugador);
        }

        [Fact]
        public void TestObtenerPlantillaJugadorPorIdInexistente()
        {
            var repository = new PlantillaJugadorRepository();
            var service = new PlantillaJugadorService(repository);

            var resultado = service.ObtenerPorId("PLA999", 999);

            Assert.Null(resultado);
        }

        [Fact]
        public void TestEliminarPlantillaJugadorExistente()
        {
            var repository = new PlantillaJugadorRepository();
            var service = new PlantillaJugadorService(repository);

            var plantillaJugador = new PlantillaJugador
            {
                IdPlantilla = "PLA001",
                IdJugador = 1,
                Numero = 10,
                EsSuplente = false
            };

            service.Agregar(plantillaJugador);

            var eliminado = service.Eliminar("PLA001", 1);

            Assert.True(eliminado);
            Assert.Null(service.ObtenerPorId("PLA001", 1));
        }

        [Fact]
        public void TestEliminarPlantillaJugadorInexistente()
        {
            var repository = new PlantillaJugadorRepository();
            var service = new PlantillaJugadorService(repository);

            var resultado = service.Eliminar("PLA999", 999);

            Assert.False(resultado);
        }
    }
}
