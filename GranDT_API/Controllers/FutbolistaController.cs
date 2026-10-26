using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_api.Contracts.Responses;
using GranDT_Clases.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/futbolistas")]
[Route("api/futbolista")]
public class FutbolistaController : ControllerBase
{
    private readonly JugadorService jugadorService;
    private readonly EquipoService equipoService;

    public FutbolistaController(JugadorService service, EquipoService equipoService)
    {
        this.jugadorService = service;
        this.equipoService = equipoService;
    }

    [HttpGet]
    public ActionResult<List<FutbolistaResponse>> ObtenerTodos([FromQuery] string? nombre)
    {
        var equipos = equipoService.ObtenerTodos().ToDictionary(equipo => equipo.IdEquipo, equipo => equipo.Nombre);
        var jugadores = string.IsNullOrWhiteSpace(nombre)
            ? jugadorService.ObtenerTodos()
            : jugadorService.ObtenerPorNombre(nombre);
        var futbolistas = jugadores
            .Select(jugador => FutbolistaResponse.Desde(
                jugador,
                jugador.IdEquipo is int idEquipo && equipos.TryGetValue(idEquipo, out var nombre) ? nombre : null))
            .ToList();

        return Ok(futbolistas);
    }

    [HttpGet("{id:int}")]
    public ActionResult<FutbolistaResponse> ObtenerPorId(int id)
    {
        var futbolista = jugadorService.ObtenerPorId(id);
        if (futbolista == null)
            return NotFound();

        return Ok(ConEquipo(futbolista));
    }

    [HttpPost]
    public IActionResult Agregar(CrearFutbolistaRequest request)
    {
        try
        {
            var nuevoFutbolista = jugadorService.Agregar(
                request.Nombre,
                request.Apellido,
                request.Apodo,
                request.Precio,
                request.FechaNacimiento,
                request.IdEquipo,
                request.IdPosicion);
            var response = ConEquipo(nuevoFutbolista);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoFutbolista.IdJugador }, response);
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Actualizar(int id, ActualizarFutbolistaRequest request)
    {
        try
        {
            var actualizado = jugadorService.Actualizar(id, request.Precio, request.IdEquipo);
            return actualizado == null ? NotFound() : Ok(ConEquipo(actualizado));
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        if (!jugadorService.Eliminar(id))
            return NotFound();

        return NoContent();
    }

    private FutbolistaResponse ConEquipo(Futbolista jugador)
    {
        if (jugador.IdEquipo is not int idEquipo)
            return FutbolistaResponse.Desde(jugador, null);

        var equipo = equipoService.ObtenerTodos().FirstOrDefault(item => item.IdEquipo == idEquipo);
        return FutbolistaResponse.Desde(jugador, equipo?.Nombre);
    }
}
