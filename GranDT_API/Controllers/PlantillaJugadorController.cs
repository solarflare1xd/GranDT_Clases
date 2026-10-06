using GranDT_Clases;
using GranDT_api.Contracts.Requests;
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

    [HttpGet("{idPlantilla:int}/{idJugador:int}")]
    public ActionResult<PlantillaJugador> ObtenerPorId(int idPlantilla, int idJugador)
    {
        var plantillaJugador = service.ObtenerPorId(idPlantilla, idJugador);

        if (plantillaJugador == null)
            return NotFound();

        return Ok(plantillaJugador);
    }

    [HttpPost]
    public ActionResult<PlantillaJugador> Agregar(AgregarJugadorAPlantillaRequest request)
    {
        var plantillaJugador = new PlantillaJugador
        {
            IdPlantilla = request.IdPlantilla,
            IdJugador = request.IdJugador,
            Numero = request.Numero,
            EsSuplente = request.EsSuplente
        };
        var nuevoRegistro = service.Agregar(plantillaJugador);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { idPlantilla = nuevoRegistro.IdPlantilla, idJugador = nuevoRegistro.IdJugador },
            nuevoRegistro);
    }

    [HttpPatch("{idPlantilla:int}/intercambiar-titulares")]
    public IActionResult IntercambiarTitularSuplente(
        int idPlantilla,
        IntercambiarTitularSuplenteRequest request)
    {
        try
        {
            if (!service.IntercambiarTitularSuplente(
                    idPlantilla,
                    request.IdJugadorTitular,
                    request.IdJugadorSuplente))
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException error)
        {
            return BadRequest(new { error = error.Message });
        }
    }

    [HttpDelete("{idPlantilla:int}/{idJugador:int}")]
    public IActionResult Eliminar(int idPlantilla, int idJugador)
    {
        if (!service.Eliminar(idPlantilla, idJugador))
            return NotFound();

        return NoContent();
    }
}