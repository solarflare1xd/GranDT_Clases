using System.Collections.Generic;
using GranDT_Clases;
using GranDT_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuntacionController : ControllerBase
{
    private readonly PuntacionService service;

    public PuntacionController(PuntacionService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<Puntacion>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{idPuntacion}")]
    public ActionResult<Puntacion> ObtenerPorId(string idPuntacion)
    {
        var puntacion = service.ObtenerPorId(idPuntacion);

        if (puntacion == null)
            return NotFound();

        return Ok(puntacion);
    }

    [HttpPost]
    public ActionResult<Puntacion> Agregar(Puntacion puntacion)
    {
        var nuevaPuntacion = service.Agregar(puntacion);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { idPuntacion = nuevaPuntacion.IdPuntacion },
            nuevaPuntacion
        );
    }

    [HttpDelete("{idPuntacion}")]
    public IActionResult Eliminar(string idPuntacion)
    {
        var eliminado = service.Eliminar(idPuntacion);

        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}

