using GranDT_api.Models;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class UsuarioRepositoryTests
    {
        [Fact]
        public void TestObtenerTodosUsuarios()
        {
            var repository = new UsuarioRepository();
            var usuarios = repository.ObtenerTodos();

            Assert.NotNull(usuarios);
            Assert.IsType<List<Usuario>>(usuarios);
        }

        [Fact]
        public void TestAgregarUsuario()
        {
            var repository = new UsuarioRepository();
            var usuario = new Usuario { Email = "test@grandt.com", Nombre = "Usuario Test" };

            var resultado = repository.Agregar(usuario);

            Assert.NotNull(resultado);
            Assert.Equal(usuario.Email, resultado.Email);
            Assert.Equal(usuario.Nombre, resultado.Nombre);
        }

        [Fact]
        public void TestObtenerUsuarioPorEmailExistente()
        {
            var repository = new UsuarioRepository();
            var usuario = new Usuario { Email = "test@grandt.com", Nombre = "Usuario Test" };
            repository.Agregar(usuario);

            var resultado = repository.ObtenerPorEmail("test@grandt.com");

            Assert.NotNull(resultado);
            Assert.Equal("test@grandt.com", resultado.Email);
        }

        [Fact]
        public void TestObtenerUsuarioPorEmailInexistente()
        {
            var repository = new UsuarioRepository();
            var resultado = repository.ObtenerPorEmail("noexiste@grandt.com");

            Assert.Null(resultado);
        }

        [Fact]
        public void TestEliminarUsuarioExistente()
        {
            var repository = new UsuarioRepository();
            var usuario = new Usuario { Email = "test@grandt.com", Nombre = "Usuario Test" };
            repository.Agregar(usuario);

            var eliminado = repository.Eliminar("test@grandt.com");

            Assert.True(eliminado);
            Assert.Null(repository.ObtenerPorEmail("test@grandt.com"));
        }

        [Fact]
        public void TestEliminarUsuarioInexistente()
        {
            var repository = new UsuarioRepository();
            var resultado = repository.Eliminar("noexiste@grandt.com");

            Assert.False(resultado);
        }
    }
}