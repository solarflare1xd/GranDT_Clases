using GranDT_Clases.Models;
using Dapper;
using Xunit;
using GranDT_Clases.IRepos;
using GranDT_Clases.Repositories;

namespace GranDT_api.Tests
{
    public class JugadorRepositoryTests
    {
        private readonly  IJugadorRepository _repository;

        public JugadorRepositoryTests()
        {
            _repository = new JugadorRepositoryMemoria();
        }

        [Fact]
        public void ObtenerTodos_DeberiaDevolverTodosLosJugadores()
        {
            // Act
            var jugadores = _repository.ObtenerTodos();

            // Assert
            Assert.NotNull(jugadores);
            Assert.IsType<List<Jugador>>(jugadores);
        }

        [Fact]
        public void Agregar_DeberiaAgregarUnJugador()
        {
            // Arrange
            var jugador = new Jugador
            {
                Id = 1,
                Nombre = "Lionel Messi"
            };

            // Act
            var resultado = _repository.Agregar(jugador);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(jugador.Id, resultado.Id);
            Assert.Equal(jugador.Nombre, resultado.Nombre);
        }

        [Fact]
        public void ObtenerPorId_DeberiaDevolverElJugador()
        {
            // Arrange
            var jugador = new Jugador
            {
                Id = 1,
                Nombre = "Lionel Messi"
            };

            _repository.Agregar(jugador);

            // Act
            var resultado = _repository.ObtenerPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("Lionel Messi", resultado.Nombre);
        }

        [Fact]
        public void ObtenerPorId_DeberiaDevolverNullSiNoExiste()
        {
            // Act
            var resultado = _repository.ObtenerPorId(999);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public void Eliminar_DeberiaEliminarElJugador()
        {
            // Arrange
            var jugador = new Jugador
            {
                Id = 1,
                Nombre = "Lionel Messi"
            };

            _repository.Agregar(jugador);

            // Act
            var eliminado = _repository.Eliminar(1);

            // Assert
            Assert.True(eliminado);
            Assert.Null(_repository.ObtenerPorId(1));
        }

        [Fact]
        public void Eliminar_DeberiaDevolverFalseSiNoExiste()
        {
            // Act
            var resultado = _repository.Eliminar(999);

            // Assert
            Assert.False(resultado);
        }
    }
}