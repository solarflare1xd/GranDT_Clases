using GranDT_api.Models;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class PlantillaRepositoryTests
    {
        [Fact]
        public void TestObtenerTodasPlantillas()
        {
            var repository = new PlantillaRepository();
            var plantillas = repository.ObtenerTodos();

            Assert.NotNull(plantillas);
            Assert.IsType<List<Plantilla>>(plantillas);
        }

        [Fact]
        public void TestAgregarPlantilla()
        {
            var repository = new PlantillaRepository();
            var plantilla = new Plantilla { IdPlantilla = "P001" };

            var resultado = repository.Agregar(plantilla);

            Assert.NotNull(resultado);
            Assert.Equal(plantilla.IdPlantilla, resultado.IdPlantilla);
        }

        [Fact]
        public void TestObtenerPlantillaPorIdExistente()
        {
            var repository = new PlantillaRepository();
            var plantilla = new Plantilla { IdPlantilla = "P001" };
            repository.Agregar(plantilla);

            var resultado = repository.ObtenerPorId("P001");

            Assert.NotNull(resultado);
            Assert.Equal("P001", resultado.IdPlantilla);
        }

        [Fact]
        public void TestObtenerPlantillaPorIdInexistente()
        {
            var repository = new PlantillaRepository();
            var resultado = repository.ObtenerPorId("P999");

            Assert.Null(resultado);
        }

        [Fact]
        public void TestEliminarPlantillaExistente()
        {
            var repository = new PlantillaRepository();
            var plantilla = new Plantilla { IdPlantilla = "P001" };
            repository.Agregar(plantilla);

            var eliminado = repository.Eliminar("P001");

            Assert.True(eliminado);
            Assert.Null(repository.ObtenerPorId("P001"));
        }

        [Fact]
        public void TestEliminarPlantillaInexistente()
        {
            var repository = new PlantillaRepository();
            var resultado = repository.Eliminar("P999");

            Assert.False(resultado);
        }
    }
}