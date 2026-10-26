using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_Clases.Services;
using GranDT_Clases.Servicios;
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
    public IActionResult Agregar(AgregarJugadorAPlantillaRequest request)
    {
        try
        {
            var nuevoRegistro = service.Agregar(
                request.IdPlantilla,
                request.IdJugador,
                request.Numero,
                request.EsSuplente);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { idPlantilla = nuevoRegistro.IdPlantilla, idJugador = nuevoRegistro.IdJugador },
                nuevoRegistro);
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpPost("/api/plantillas/{id:int}/jugadores")]
    public IActionResult AgregarAPlantilla(int id, AgregarJugadorAPlantillaRequest request)
    {
        try
        {
            var actualizada = service.AgregarAPlantilla(
                id,
                request.IdJugador,
                request.Numero,
                request.EsSuplente);
            return actualizada == null ? NotFound() : Ok(actualizada);
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpPut("/api/plantillas/{id:int}/jugadores/{futbolistaId:int}")]
    public IActionResult ActualizarCondicion(
        int id,
        int futbolistaId,
        ActualizarCondicionJugadorRequest request)
    {
        try
        {
            var actualizado = service.ActualizarCondicion(id, futbolistaId, request.EsSuplente!.Value);
            return actualizado == null ? NotFound() : Ok(actualizado);
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
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