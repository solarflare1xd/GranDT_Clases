using GranDT_Clases;
using GranDT_Clases.Services;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuntuacionController : ControllerBase
{
    private readonly PuntuacionService service;

    public PuntuacionController(PuntuacionService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<Puntuacion>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{id}")]
    public ActionResult<Puntuacion> ObtenerPorId(string id)
    {
        var puntuacion = service.ObtenerPorId(id);

        if (puntuacion == null)
            return NotFound();

        return Ok(puntuacion);
    }

    [HttpPost]
    public ActionResult<Puntuacion> Agregar(Puntuacion puntuacion, [FromQuery] int idJugador)
    {
        var nuevaPuntuacion = service.Agregar(puntuacion, idJugador);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPuntuacion.IdPuntuacion }, nuevaPuntuacion);
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(string id)
    {
        if (!service.Eliminar(id))
            return NotFound();

        return NoContent();
    }
}