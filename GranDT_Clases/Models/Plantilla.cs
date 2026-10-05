namespace GranDT_Clases;

public class Plantilla
{
    public string IdPlantilla { get; set; } = string.Empty;
    public decimal Presupuesto { get; set; }
    public int CantidadMaximaJugadores { get; set; } = 20;
    public List<Futbolista> Jugadores { get; set; } = new();
    public List<Futbolista> Jugadores_sup { get; set; } = new();

    public decimal Gasto => Jugadores.Concat(Jugadores_sup).Sum(j => j.Precio);
    public decimal PresupuestoDisponible => Presupuesto - Gasto;
    public int CantidadJugadores => Jugadores.Count + Jugadores_sup.Count;

    public bool PresupuestoValido => Gasto <= Presupuesto;

    public bool CantidadValida => CantidadJugadores <= CantidadMaximaJugadores;

    public bool FormacionValida =>
        Jugadores.Count(j => j.Posicion?.Nombre == "Arquero") == 1
        && Jugadores.Count(j => j.Posicion?.Nombre == "Defensor") == 4
        && Jugadores.Count(j => j.Posicion?.Nombre == "Mediocampista") == 3
        && Jugadores.Count(j => j.Posicion?.Nombre == "Delantero") == 2;

    public bool EsValida => PresupuestoValido && CantidadValida && FormacionValida;

    public decimal PuntajeFecha(int fecha)
    {
        if (fecha is < 1 or >= 50)
        {
            throw new ArgumentOutOfRangeException(nameof(fecha), "La fecha debe estar entre 1 y 49.");
        }

        return Jugadores
            .SelectMany(j => j.HistorialPuntajes)
            .Where(p => p.Fecha == fecha)
            .Sum(p => p.Puntaje);
    }
}
