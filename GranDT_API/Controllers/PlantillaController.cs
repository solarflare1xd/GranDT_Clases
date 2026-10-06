using GranDT_Clases;
using GranDT_api.Contracts.Requests;
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

    [HttpGet("usuario/{email}")]
    public ActionResult<Plantilla> ObtenerPorUsuario(string email)
    {
        var plantilla = service.ObtenerPorUsuario(email);

        if (plantilla == null)
            return NotFound();

        return Ok(plantilla);
    }

    [HttpPost]
    public ActionResult<Plantilla> Agregar(CrearPlantillaRequest request)
    {
        try
        {
            var integrantes = (request.Jugadores ?? new()).Select(jugador => new PlantillaJugador
            {
                IdJugador = jugador.IdJugador,
                Numero = jugador.Numero,
                EsSuplente = jugador.EsSuplente
            }).ToList();

            var nuevaPlantilla = service.CrearCompleta(request.IdPlantilla, request.Presupuesto, integrantes);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPlantilla.IdPlantilla }, nuevaPlantilla);
        }
        catch (ArgumentException error)
        {
            return BadRequest(new { error = error.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(string id)
    {
        if (!service.Eliminar(id))
            return NotFound();

        return NoContent();
    }
}