using GranDT_api.Models;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class EquipoRepositoryTests
    {
        [Fact]
        public void TestObtenerTodosEquipos()
        {
            var repository = new EquipoRepository();
            var equipos = repository.ObtenerTodos();

            Assert.NotNull(equipos);
            Assert.IsType<List<Equipo>>(equipos);
        }

        [Fact]
        public void TestAgregarEquipo()
        {
            var repository = new EquipoRepository();
            var equipo = new Equipo { Nombre = "Argentina" };

            var resultado = repository.Agregar(equipo);

            Assert.NotNull(resultado);
            Assert.Equal(equipo.Nombre, resultado.Nombre);
        }

        [Fact]
        public void TestObtenerEquipoPorIdExistente()
        {
            var repository = new EquipoRepository();
            var equipo = new Equipo { Nombre = "Argentina" };
            repository.Agregar(equipo);

            var resultado = repository.ObtenerPorId("Argentina");

            Assert.NotNull(resultado);
            Assert.Equal("Argentina", resultado.Nombre);
        }

        [Fact]
        public void TestObtenerEquipoPorIdInexistente()
        {
            var repository = new EquipoRepository();
            var resultado = repository.ObtenerPorId("Brasil");

            Assert.Null(resultado);
        }

        [Fact]
        public void TestEliminarEquipoExistente()
        {
            var repository = new EquipoRepository();
            var equipo = new Equipo { Nombre = "Argentina" };
            repository.Agregar(equipo);

            var eliminado = repository.Eliminar("Argentina");

            Assert.True(eliminado);
            Assert.Null(repository.ObtenerPorId("Argentina"));
        }

        [Fact]
        public void TestEliminarEquipoInexistente()
        {
            var repository = new EquipoRepository();
            var resultado = repository.Eliminar("Brasil");

            Assert.False(resultado);
        }
    }
}