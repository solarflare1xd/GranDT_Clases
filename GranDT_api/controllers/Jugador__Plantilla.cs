
using GranDT_Clases;
using GranDT_Clases.Services;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaJugadorController : ControllerBase
{
    private readonly PlantillaJugadorService service;

    public PlantillaJugadorController(PlantillaJugadorService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<PlantillaJugador>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{idPlantilla}/{idJugador}")]
    public ActionResult<PlantillaJugador> ObtenerPorId(
        string idPlantilla,
        int idJugador)
    {
        var plantillaJugador = service.ObtenerPorId(idPlantilla, idJugador);

        if (plantillaJugador == null)
            return NotFound();

        return Ok(plantillaJugador);
    }

    [HttpPost]
    public ActionResult<PlantillaJugador> Agregar(
        PlantillaJugador plantillaJugador)
    {
        var nuevo = service.Agregar(plantillaJugador);

        return Ok(nuevo);
    }

    [HttpDelete("{idPlantilla}/{idJugador}")]
    public IActionResult Eliminar(
        string idPlantilla,
        int idJugador)
    {
        var eliminado = service.Eliminar(idPlantilla, idJugador);

        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}


