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

    [HttpGet("{id:int}")]
    public ActionResult<Plantilla> ObtenerPorId(int id)
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
            var titulares = request.Titulares ?? new();
            var suplentes = request.Suplentes ?? new();
            var integrantes = titulares
                .Select((idJugador, indice) => new PlantillaJugador
                {
                    IdJugador = idJugador,
                    Numero = indice + 1,
                    EsSuplente = false
                })
                .Concat(suplentes.Select((idJugador, indice) => new PlantillaJugador
                {
                    IdJugador = idJugador,
                    Numero = titulares.Count + indice + 1,
                    EsSuplente = true
                }))
                .ToList();

            var nuevaPlantilla = service.CrearCompleta(request.Presupuesto, integrantes);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPlantilla.IdPlantilla }, nuevaPlantilla);
        }
        catch (ArgumentException error)
        {
            return BadRequest(new { error = error.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        if (!service.Eliminar(id))
            return NotFound();

        return NoContent();
    }
}