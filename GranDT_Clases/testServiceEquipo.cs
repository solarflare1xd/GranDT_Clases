
using GranDT_Clases;
using GranDT_api.Repositories;
using GranDT_api.Services;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class EquipoServiceTests
    {
        [Fact]
        public void TestObtenerTodosEquipos()
        {
            var repository = new EquipoRepository();
            var service = new EquipoService(repository);

            var equipos = service.ObtenerTodos();

            Assert.NotNull(equipos);
            Assert.IsType<List<Equipo>>(equipos);
        }

        [Fact]
        public void TestAgregarEquipo()
        {
            var repository = new EquipoRepository();
            var service = new EquipoService(repository);

            var equipo = new Equipo { Nombre = "River Plate" };

            var resultado = service.Agregar(equipo);

            Assert.NotNull(resultado);
            Assert.Equal(equipo.Nombre, resultado.Nombre);
        }

        [Fact]
        public void TestObtenerEquipoPorNombreExistente()
        {
            var repository = new EquipoRepository();
            var service = new EquipoService(repository);

            var equipo = new Equipo { Nombre = "River Plate" };
            service.Agregar(equipo);

            var resultado = service.ObtenerPorNombre("River Plate");

            Assert.NotNull(resultado);
            Assert.Equal("River Plate", resultado.Nombre);
        }

        [Fact]
        public void TestObtenerEquipoPorNombreInexistente()
        {
            var repository = new EquipoRepository();
            var service = new EquipoService(repository);

            var resultado = service.ObtenerPorNombre("Equipo Inexistente");

            Assert.Null(resultado);
        }

        [Fact]
        public void TestEliminarEquipoExistente()
        {
            var repository = new EquipoRepository();
            var service = new EquipoService(repository);

            var equipo = new Equipo { Nombre = "River Plate" };
            service.Agregar(equipo);

            var eliminado = service.Eliminar("River Plate");

            Assert.True(eliminado);
            Assert.Null(service.ObtenerPorNombre("River Plate"));
        }

        [Fact]
        public void TestEliminarEquipoInexistente()
        {
            var repository = new EquipoRepository();
            var service = new EquipoService(repository);

            var resultado = service.Eliminar("Equipo Inexistente");

            Assert.False(resultado);
        }
    }
}
