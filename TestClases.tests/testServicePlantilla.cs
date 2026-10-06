using GranDT_Clases;
using GranDT_Clases.Services;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class PlantillaServiceTests
    {
        [Fact]
        public void TestObtenerTodasPlantillas()
        {
            var repository = new FakePlantillaRepository();
            var service = new PlantillaService(repository);

            var plantillas = service.ObtenerTodos();

            Assert.NotNull(plantillas);
            Assert.IsType<List<Plantilla>>(plantillas);
        }

        [Fact]
        public void TestAgregarPlantilla()
        {
            var repository = new FakePlantillaRepository();
            var service = new PlantillaService(repository);

            var plantilla = new Plantilla
            {
                IdPlantilla = 1,
                Presupuesto = 100000
            };

            var resultado = service.Agregar(plantilla);

            Assert.NotNull(resultado);
            Assert.Equal(plantilla.IdPlantilla, resultado.IdPlantilla);
            Assert.Equal(plantilla.Presupuesto, resultado.Presupuesto);
        }

        [Fact]
        public void TestObtenerPlantillaPorIdExistente()
        {
            var repository = new FakePlantillaRepository();
            var service = new PlantillaService(repository);

            var plantilla = new Plantilla
            {
                IdPlantilla = 1,
                Presupuesto = 100000
            };

            service.Agregar(plantilla);

            var resultado = service.ObtenerPorId(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdPlantilla);
        }

        [Fact]
        public void TestObtenerPlantillaPorIdInexistente()
        {
            var repository = new FakePlantillaRepository();
            var service = new PlantillaService(repository);

            var resultado = service.ObtenerPorId(999);

            Assert.Null(resultado);
        }

        [Fact]
        public void TestEliminarPlantillaExistente()
        {
            var repository = new FakePlantillaRepository();
            var service = new PlantillaService(repository);

            var plantilla = new Plantilla
            {
                IdPlantilla = 1,
                Presupuesto = 100000
            };

            service.Agregar(plantilla);

            var eliminado = service.Eliminar(1);

            Assert.True(eliminado);
            Assert.Null(service.ObtenerPorId(1));
        }

        [Fact]
        public void TestEliminarPlantillaInexistente()
        {
            var repository = new FakePlantillaRepository();
            var service = new PlantillaService(repository);

            var resultado = service.Eliminar(999);

            Assert.False(resultado);
        }

        [Fact]
        public void CrearCompletaAceptaFormacionConTresMediocampistasYTresDelanteros()
        {
            var repository = new FakePlantillaRepository();
            var jugadorRepository = new FakeJugadorRepository();
            var posiciones = new[]
            {
                "Arquero",
                "Defensor", "Defensor", "Defensor", "Defensor",
                "Mediocampista", "Mediocampista", "Mediocampista",
                "Delantero", "Delantero", "Delantero"
            };

            for (var indice = 0; indice < posiciones.Length; indice++)
            {
                jugadorRepository.Agregar(new Futbolista
                {
                    IdJugador = indice + 1,
                    Precio = 1,
                    Posicion = new Posicion { Nombre = posiciones[indice] }
                });
            }

            var integrantes = Enumerable.Range(1, posiciones.Length)
                .Select(idJugador => new PlantillaJugador { IdJugador = idJugador })
                .ToList();

            var service = new PlantillaService(repository, jugadorRepository);
            var plantilla = service.CrearCompleta(100, integrantes);

            Assert.Equal(11, plantilla.Jugadores.Count);
            Assert.Equal(1, plantilla.IdPlantilla);
        }

        [Fact]
        public void CrearCompletaAceptaPlantillaVaciaParaAgregarJugadoresDespues()
        {
            var repository = new FakePlantillaRepository();
            var service = new PlantillaService(repository, new FakeJugadorRepository());

            var plantilla = service.CrearCompleta(100, Array.Empty<PlantillaJugador>());

            Assert.Empty(plantilla.Jugadores);
            Assert.Empty(plantilla.Jugadores_sup);
            Assert.Equal(1, plantilla.IdPlantilla);
        }
    }
}
