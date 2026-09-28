using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Repositories;
using GranDT_Clases.Servicios;
using Xunit;
using System.Collections.Generic;

namespace GranDT_api.Tests
{
    public class UsuarioServiceTests
    {
        [Fact]
        public void TestObtenerTodosUsuarios()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var usuarios = service.ObtenerTodos();

            Assert.NotNull(usuarios);
            Assert.IsType<List<Usuario>>(usuarios);
        }

        [Fact]
        public void TestAgregarUsuario()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var usuario = new Usuario
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Email = "juan@gmail.com",
                Nacimiento = new DateOnly(2005, 5, 10),
                Password = "12345678",
                EsAdministrador = false
            };

            var resultado = service.Agregar(usuario);

            Assert.NotNull(resultado);
            Assert.Equal(usuario.Email, resultado.Email);
            Assert.Equal(usuario.Nombre, resultado.Nombre);

            Assert.NotEqual("12345678", resultado.Password);
            Assert.True(
                BCrypt.Net.BCrypt.Verify("12345678", resultado.Password)
            );
        }

        [Fact]
        public void TestObtenerUsuarioPorEmailExistente()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var usuario = new Usuario
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Email = "juan@gmail.com",
                Nacimiento = new DateOnly(2005, 5, 10),
                Password = "12345678"
            };

            service.Agregar(usuario);

            var resultado = service.ObtenerPor("juan@gmail.com");

            Assert.NotNull(resultado);
            Assert.Equal("juan@gmail.com", resultado.Email);
        }

        [Fact]
        public void TestObtenerUsuarioPorEmailInexistente()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var resultado = service.ObtenerPor("inexistente@gmail.com");

            Assert.Null(resultado);
        }

        [Fact]
        public void TestAgregarUsuarioConPasswordInvalida()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var usuario = new Usuario
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Email = "juan@gmail.com",
                Nacimiento = new DateOnly(2005, 5, 10),
                Password = "123"
            };

            Assert.Throws<System.Exception>(() => service.Agregar(usuario));
        }

        [Fact]
        public void TestAgregarUsuarioConEmailInvalido()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var usuario = new Usuario
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Email = "juan.com",
                Nacimiento = new DateOnly(2005, 5, 10),
                Password = "12345678"
            };

            Assert.Throws<System.Exception>(() => service.Agregar(usuario));
        }

        [Fact]
        public void TestAgregarUsuarioExistente()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var usuario = new Usuario
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Email = "juan@gmail.com",
                Nacimiento = new DateOnly(2005, 5, 10),
                Password = "12345678"
            };

            service.Agregar(usuario);

            var usuarioRepetido = new Usuario
            {
                Nombre = "Pedro",
                Apellido = "Gomez",
                Email = "juan@gmail.com",
                Nacimiento = new DateOnly(2004, 3, 20),
                Password = "abcdefgh"
            };

            Assert.Throws<System.Exception>(() => service.Agregar(usuarioRepetido));
        }

        [Fact]
        public void TestEliminarUsuarioExistente()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var usuario = new Usuario
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Email = "juan@gmail.com",
                Nacimiento = new DateOnly(2005, 5, 10),
                Password = "12345678"
            };

            service.Agregar(usuario);

            var eliminado = service.Eliminar("juan@gmail.com");

            Assert.True(eliminado);
            Assert.Null(service.ObtenerPor("juan@gmail.com"));
        }

        [Fact]
        public void TestEliminarUsuarioInexistente()
        {
            var repository = new UsuarioRepositoryMemoria();
            var service = new UsuarioService(repository);

            var resultado = service.Eliminar("inexistente@gmail.com");

            Assert.False(resultado);
        }
    }
}


