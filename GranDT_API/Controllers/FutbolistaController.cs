using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_Clases.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FutbolistaController : ControllerBase
{
    private readonly JugadorService service;

    public FutbolistaController(JugadorService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<Futbolista>> ObtenerTodos([FromQuery] string? nombre)
    {
        if (!string.IsNullOrWhiteSpace(nombre))
        {
            return Ok(service.ObtenerPorNombre(nombre));
        }

        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Futbolista> ObtenerPorId(int id)
    {
        var futbolista = service.ObtenerPorId(id);

        if (futbolista == null)
            return NotFound();

        return Ok(futbolista);
    }

    [HttpPost]
    public ActionResult<Futbolista> Agregar(CrearFutbolistaRequest request)
    {
        var futbolista = new Futbolista
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Apodo = request.Apodo,
            Precio = request.Precio,
            FechaNacimiento = request.FechaNacimiento,
            IdEquipo = request.IdEquipo,
            Posicion = new Posicion { IdPosicion = request.IdPosicion }
        };
        var nuevoFutbolista = service.Agregar(futbolista);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoFutbolista.IdJugador }, nuevoFutbolista);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        if (!service.Eliminar(id))
            return NotFound();

        return NoContent();
    }
}