using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_Clases.Services;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuntuacionController : ControllerBase
{
    private readonly PuntuacionService service;

    public PuntuacionController(PuntuacionService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<Puntuacion>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{id}")]
    public ActionResult<Puntuacion> ObtenerPorId(string id)
    {
        var puntuacion = service.ObtenerPorId(id);

        if (puntuacion == null)
            return NotFound();

        return Ok(puntuacion);
    }

    [HttpPost]
    public IActionResult Agregar(CrearPuntuacionRequest request)
    {
        try
        {
            var nuevaPuntuacion = service.Agregar(
                request.IdPuntuacion,
                request.Fecha,
                request.Puntaje,
                request.IdJugador);
            if (nuevaPuntuacion == null)
                return NotFound();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPuntuacion.IdPuntuacion }, nuevaPuntuacion);
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpPost("/api/futbolistas/{id:int}/puntuaciones")]
    public IActionResult AgregarAFutbolista(int id, CrearPuntuacionRequest request)
    {
        try
        {
            var nuevaPuntuacion = service.Agregar(
                request.IdPuntuacion,
                request.Fecha,
                request.Puntaje,
                id);
            if (nuevaPuntuacion == null)
                return NotFound();

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = nuevaPuntuacion.IdPuntuacion },
                nuevaPuntuacion);
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(string id)
    {
        try
        {
            if (!service.Eliminar(id))
                return NotFound();

            return NoContent();
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }
}