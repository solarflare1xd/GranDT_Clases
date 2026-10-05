using System.Collections.Generic;
using GranDT_Clases;
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
    public ActionResult<Equipo> Agregar(Equipo equipo)
    {
        // El repositorio ejecuta ExecuteScalar y le asigna el ID generado a la entidad
        var nuevoEquipo = service.Agregar(equipo);

        // Devuelve HTTP 201 (Created), la ruta para consultar el equipo y el objeto con su ID escalar
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