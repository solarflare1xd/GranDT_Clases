using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GranDT_Clases;

public class Puntuacion
{
    public string IdPuntuacion { get; set; } = string.Empty;
    [Range(1, 49)]
    public int Fecha { get; set; }

    [Range(typeof(decimal), "1", "10")]
    public decimal Puntaje { get; set; }
    public int IdJugador { get; set; }

    [JsonIgnore]
    public DateOnly Partido_date { get; set; }
}
