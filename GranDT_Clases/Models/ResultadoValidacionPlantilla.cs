namespace GranDT_Clases;

public sealed class ResultadoValidacionPlantilla
{
    public string IdPlantilla { get; set; } = string.Empty;
    public bool PresupuestoValido { get; set; }
    public bool CantidadValida { get; set; }
    public bool FormacionValida { get; set; }
    public decimal Gasto { get; set; }
    public decimal PresupuestoDisponible { get; set; }
    public int CantidadJugadores { get; set; }

    public bool EsValida => PresupuestoValido && CantidadValida && FormacionValida;
}
