using GranDT_Clases.IRepos;



namespace GranDT_Clases.Servicios;


public class JugadorService
{
    private readonly IJugadorRepository repository;

    public JugadorService(IJugadorRepository repository)
    {
        this.repository = repository;
    }

    public List<Futbolista> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Futbolista? ObtenerPorId(int id)
    {
        return repository.ObtenerPorId(id);
    }

    public List<Futbolista> ObtenerPorNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));
        }

        return repository.ObtenerPorNombre(nombre.Trim());
    }

    public Futbolista Agregar(Futbolista futbolista)
    {
        return repository.Agregar(futbolista);
    }

    public Futbolista Agregar(
        string nombre,
        string apellido,
        string? apodo,
        decimal precio,
        DateOnly fechaNacimiento,
        int? idEquipo,
        int idPosicion)
    {
        if (fechaNacimiento == default || fechaNacimiento > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("La fecha de nacimiento debe ser válida y no futura.");
        if (precio <= 0)
            throw new ArgumentException("La cotización debe ser mayor que cero.");

        return repository.Agregar(new Futbolista
        {
            Nombre = nombre.Trim(),
            Apellido = apellido.Trim(),
            Apodo = apodo?.Trim(),
            Precio = precio,
            FechaNacimiento = fechaNacimiento,
            IdEquipo = idEquipo,
            Posicion = new Posicion { IdPosicion = idPosicion }
        });
    }

    public Futbolista? Actualizar(Futbolista futbolista)
    {
        return repository.Actualizar(futbolista);
    }

    public Futbolista? Actualizar(int id, decimal precio, int? idEquipo)
    {
        if (precio <= 0)
            throw new ArgumentException("La cotización debe ser mayor que cero.");

        var futbolista = repository.ObtenerPorId(id);
        if (futbolista == null)
            return null;

        futbolista.Precio = precio;
        futbolista.IdEquipo = idEquipo;
        return repository.Actualizar(futbolista);
    }

    public bool Eliminar(int id)
    {
        return repository.Eliminar(id);
    }
}