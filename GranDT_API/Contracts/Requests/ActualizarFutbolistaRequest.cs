using System.ComponentModel.DataAnnotations;

namespace GranDT_api.Contracts.Requests;

public sealed class ActualizarFutbolistaRequest
{
    [Range(typeof(decimal), "0.01", "99999999.99")]
    public decimal Precio { get; set; }

    [Range(1, int.MaxValue)]
    public int? IdEquipo { get; set; }
}
