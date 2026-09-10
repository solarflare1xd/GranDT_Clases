namespace BibliotecaApi.Models;

public class Jugador
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Apodo { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; } = new DateOnly();
    public string Team { get; set; } = string.Empty;
}