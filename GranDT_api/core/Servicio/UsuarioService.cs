using GranDT_Clases;
using GranDT_Clases.Repositories;
using BCrypt.Net;
using System.Text.RegularExpressions;

namespace GranDT_Clases.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        this.repository = repository;
    }

    public List<Usuario> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Usuario? ObtenerPorEmailYPassword(string email, string password)
    {
        Usuario? usuario = repository.ObtenerPorEmail(email);

        if (usuario == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(password, usuario.Password))
            return null;

        return usuario;
    }

    public Usuario Agregar(Usuario usuario)
    {
        if (!DatosCorrectos(usuario))
            throw new Exception("Los datos del usuario no son correctos.");

        usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

        return repository.Agregar(usuario);
    }

    public bool Eliminar(string email, string password)
    {
        Usuario? usuario = ObtenerPorEmailYPassword(email, password);

        if (usuario == null)
            return false;

        return repository.Eliminar(email);
    }

            private bool DatosCorrectos(Usuario usuario)
            {
                if (string.IsNullOrWhiteSpace(usuario.Nombre))
                    return false;

                if (string.IsNullOrWhiteSpace(usuario.Apellido))
                    return false;

                if (string.IsNullOrWhiteSpace(usuario.Email))
                    return false;

                if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    return false;

                if (string.IsNullOrWhiteSpace(usuario.Password))
                    return false;

                if (usuario.Password.Length < 8)
                    return false;

                return true;
            }
        }