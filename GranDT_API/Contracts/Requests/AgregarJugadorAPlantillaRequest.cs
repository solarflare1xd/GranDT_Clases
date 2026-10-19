using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class AgregarJugadorAPlantillaRequest
{
    public int IdPlantilla { get; set; }
    [Range(1, int.MaxValue)]
    public int IdJugador { get; set; }
    [Range(1, 99)]
    public int Numero { get; set; }
    public bool EsSuplente { get; set; }
}
