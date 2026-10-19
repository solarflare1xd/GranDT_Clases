using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_Clases.Services;
using GranDT_Clases.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuntuacionController : ControllerBase
{
    private readonly PuntuacionService service;
    private readonly JugadorService jugadorService;

    public PuntuacionController(PuntuacionService service, JugadorService jugadorService)
    {
        this.service = service;
        this.jugadorService = jugadorService;
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
    public ActionResult<Puntuacion> Agregar(CrearPuntuacionRequest request)
    {
        var puntuacion = new Puntuacion
        {
            IdPuntuacion = request.IdPuntuacion,
            Fecha = request.Fecha,
            Puntaje = request.Puntaje,
            IdJugador = request.IdJugador
        };
        var nuevaPuntuacion = service.Agregar(puntuacion, request.IdJugador);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPuntuacion.IdPuntuacion }, nuevaPuntuacion);
    }

    [HttpPost("/api/futbolistas/{id:int}/puntuaciones")]
    public IActionResult AgregarAFutbolista(int id, CrearPuntuacionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.IdPuntuacion)
            || request.Fecha is < 1 or > 49
            || request.Puntaje is < 1 or > 10)
        {
            return BadRequest(new { error = "La puntuación, fecha e identificador no son válidos." });
        }

        if (jugadorService.ObtenerPorId(id) == null)
            return NotFound();

        var puntuacion = new Puntuacion
        {
            IdPuntuacion = request.IdPuntuacion.Trim(),
            Fecha = request.Fecha,
            Puntaje = request.Puntaje,
            IdJugador = id
        };

        try
        {
            var nuevaPuntuacion = service.Agregar(puntuacion, id);
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
        if (!service.Eliminar(id))
            return NotFound();

        return NoContent();
    }
}