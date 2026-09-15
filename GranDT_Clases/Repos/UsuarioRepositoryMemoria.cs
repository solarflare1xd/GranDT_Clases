using System.Collections.Generic;
using System.Linq;
using GranDT_Clases.IRepos;
namespace GranDT_Clases.Repositories;

public class UsuarioRepositoryMemoria : IUsuarioRepository
{
    private readonly List<Usuario> usuarios = new();

    public List<Usuario> ObtenerTodos()
    {
        return usuarios;
    }

    public Usuario? ObtenerPorEmail(string email)
    {
        return usuarios.FirstOrDefault(u => u.Email == email);
    }

    public Usuario Agregar(Usuario usuario)
    {
        usuarios.Add(usuario);

        return usuario;
    }

    public bool Eliminar(string email)
    {
        var usuario = ObtenerPorEmail(email);

        if (usuario == null)
            return false;

        usuarios.Remove(usuario);

        return true;
    }
}