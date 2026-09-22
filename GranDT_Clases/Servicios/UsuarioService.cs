using System.Collections.Generic;
using GranDT_Clases;
using GranDT_Clases.IRepos;
using Org.BouncyCastle.Crypto.Generators;

namespace GranDT_Clases.Servicios ;

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

    public Usuario? ObtenerPor(string email)
    {
        return repository.ObtenerPorEmail(email);
    }
    public bool datosval(string email, string password)
    {
        
        if (password.Length < 8)
        {
            return false;
        }
        if (!email.Contains("@"))
        {
            return false;
        }

        return true;
        
    }
    public Usuario Agregar(Usuario usuario)
    {
        if (!datosval(usuario.Email, usuario.Password))
        {
            throw new Exception("Datos inválidos");
        }

        usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

        if (repository.ObtenerPorEmail(usuario.Email) != null)
        {
            throw new Exception("El usuario ya existe");
        }
        return repository.Agregar(usuario);
    }

    public bool Eliminar(string email)
    {
        return repository.Eliminar(email);
    }
    
}