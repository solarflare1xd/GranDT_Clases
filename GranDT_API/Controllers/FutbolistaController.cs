using GranDT_Clases;
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
    public ActionResult<List<Futbolista>> ObtenerTodos()
    {
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
    public ActionResult<Futbolista> Agregar(Futbolista futbolista)
    {
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