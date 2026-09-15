namespace GranDT_Clases.IRepos;

public interface IUsuarioRepository
{
    List<Usuario> ObtenerTodos();
    Usuario? ObtenerPorEmail(string email);
    Usuario Agregar(Usuario usuario);
    bool Eliminar(string email);
}