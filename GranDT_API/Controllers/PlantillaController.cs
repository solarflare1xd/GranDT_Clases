using GranDT_Clases;
using GranDT_Clases.Services;
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

    [HttpGet("{id}")]
    public ActionResult<Plantilla> ObtenerPorId(string id)
    {
        var plantilla = service.ObtenerPorId(id);

        if (plantilla == null)
            return NotFound();

        return Ok(plantilla);
    }

    [HttpPost]
    public ActionResult<Plantilla> Agregar(Plantilla plantilla)
    {
        var nuevaPlantilla = service.Agregar(plantilla);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPlantilla.IdPlantilla }, nuevaPlantilla);
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(string id)
    {
        if (!service.Eliminar(id))
            return NotFound();

        return NoContent();
    }
}