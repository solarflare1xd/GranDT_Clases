using System.Collections.Generic;
using GranDT_Clases;
using GranDT_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly PlantillaService service;

    public PlantillaController(PlantillaService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<Plantilla>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{idPlantilla}")]
    public ActionResult<Plantilla> ObtenerPorId(string idPlantilla)
    {
        var plantilla = service.ObtenerPorId(idPlantilla);

        if (plantilla == null)
            return NotFound();

        return Ok(plantilla);
    }

    [HttpPost]
    public ActionResult<Plantilla> Agregar(Plantilla plantilla)
    {
        var nuevaPlantilla = service.Agregar(plantilla);

        return Ok(nuevaPlantilla);
    }

    [HttpDelete("{idPlantilla}")]
    public IActionResult Eliminar(string idPlantilla)
    {
        var eliminado = service.Eliminar(idPlantilla);

        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}