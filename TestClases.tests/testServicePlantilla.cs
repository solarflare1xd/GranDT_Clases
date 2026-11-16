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
        public void CrearCompletaAceptaFormacionConCuatroMediocampistasYDosDelanteros()
        {
            var repository = new FakePlantillaRepository();
            var jugadorRepository = new FakeJugadorRepository();
            var posiciones = new[]
            {
                "Arquero",
                "Defensor", "Defensor", "Defensor", "Defensor",
                "Mediocampista", "Mediocampista", "Mediocampista", "Mediocampista",
                "Delantero", "Delantero"
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

        [Fact]
        public void ObtenerPuntajeFechaSumaTitularesYDevuelveCeroSiNoHayPuntajes()
        {
            var repository = new FakePlantillaRepository();
            repository.Agregar(new Plantilla
            {
                IdPlantilla = 1,
                Presupuesto = 100,
                Jugadores = new List<Futbolista>
                {
                    new()
                    {
                        IdJugador = 1,
                        HistorialPuntajes = new List<Puntuacion>
                        {
                            new() { Fecha = 1, Puntaje = 8.5m }
                        }
                    },
                    new() { IdJugador = 2 }
                },
                Jugadores_sup = new List<Futbolista>
                {
                    new()
                    {
                        IdJugador = 3,
                        HistorialPuntajes = new List<Puntuacion>
                        {
                            new() { Fecha = 1, Puntaje = 10 }
                        }
                    }
                }
            });
            var service = new PlantillaService(repository);

            Assert.Equal(8.5m, service.ObtenerPuntajeFecha(1, 1)!.Value);
            Assert.Equal(0m, service.ObtenerPuntajeFecha(1, 2)!.Value);
            Assert.Null(service.ObtenerPuntajeFecha(999, 1));
        }

        [Fact]
        public void ValidarPlantillaInformaFormacionCompletaYPlantillaInexistente()
        {
            var repository = new FakePlantillaRepository();
            var titulares = new[]
            {
                "Arquero",
                "Defensor", "Defensor", "Defensor", "Defensor",
                "Mediocampista", "Mediocampista", "Mediocampista", "Mediocampista",
                "Delantero", "Delantero"
            };
            repository.Agregar(new Plantilla
            {
                IdPlantilla = 1,
                Presupuesto = 100,
                CantidadMaximaJugadores = 20,
                Jugadores = titulares.Select((posicion, indice) => new Futbolista
                {
                    IdJugador = indice + 1,
                    Precio = 1,
                    Posicion = new Posicion { Nombre = posicion }
                }).ToList()
            });
            repository.Agregar(new Plantilla
            {
                IdPlantilla = 2,
                Presupuesto = 100,
                CantidadMaximaJugadores = 20,
                Jugadores = new List<Futbolista>
                {
                    new() { IdJugador = 12, Precio = 1, Posicion = new Posicion { Nombre = "Arquero" } }
                }
            });
            var service = new PlantillaService(repository);

            Assert.Empty(service.ObtenerErroresValidacion(1)!);
            Assert.Contains(
                service.ObtenerErroresValidacion(2)!,
                error => error.Contains("exactamente", StringComparison.Ordinal));
            Assert.Null(service.ObtenerErroresValidacion(999));
        }

        [Fact]
        public void ObtenerPuntajeFechaRechazaFechasFueraDelRango()
        {
            var service = new PlantillaService(new FakePlantillaRepository());

            Assert.Throws<ArgumentOutOfRangeException>(() => service.ObtenerPuntajeFecha(1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => service.ObtenerPuntajeFecha(1, 50));
        }
    }
}
