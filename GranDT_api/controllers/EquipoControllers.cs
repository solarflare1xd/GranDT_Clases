using System.Collections.Generic;
using GranDT_Clases;
using GranDT_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    private readonly EquipoService service;

    public EquipoController(EquipoService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<Equipo>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{nombre}")]
    public ActionResult<Equipo> ObtenerPorNombre(string nombre)
    {
        var equipo = service.ObtenerPorId(nombre);

        if (equipo == null)
            return NotFound();

        return Ok(equipo);
    }

    [HttpPost]
    public ActionResult<Equipo> Agregar(Equipo equipo)
    {
        var nuevoEquipo = service.Agregar(equipo);

        return Ok(nuevoEquipo);
    }

    [HttpDelete("{nombre}")]
    public IActionResult Eliminar(string nombre)
    {
        var eliminado = service.Eliminar(nombre);

        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}