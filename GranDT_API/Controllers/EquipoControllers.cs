using System.Collections.Generic;
using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_Clases.Servicios;
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
        
        var equipo = service.ObtenerPorNombre(nombre); 

        if (equipo == null)
            return NotFound();

        return Ok(equipo);
    }

    [HttpPost]
    public ActionResult<Equipo> Agregar(CrearEquipoRequest request)
    {
        var equipo = new Equipo { Nombre = request.Nombre };
        var nuevoEquipo = service.Agregar(equipo);

        return CreatedAtAction(nameof(ObtenerPorNombre), new { nombre = nuevoEquipo.Nombre }, nuevoEquipo);
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