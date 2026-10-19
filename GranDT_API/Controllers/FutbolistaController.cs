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
    private readonly JugadorService service;
    private readonly EquipoService equipoService;

    public FutbolistaController(JugadorService service, EquipoService equipoService)
    {
        this.service = service;
        this.equipoService = equipoService;
    }

    [HttpGet]
    public ActionResult<List<FutbolistaResponse>> ObtenerTodos([FromQuery] string? nombre)
    {
        var equipos = equipoService.ObtenerTodos().ToDictionary(equipo => equipo.IdEquipo, equipo => equipo.Nombre);
        var jugadores = string.IsNullOrWhiteSpace(nombre)
            ? service.ObtenerTodos()
            : service.ObtenerPorNombre(nombre);
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
        var futbolista = service.ObtenerPorId(id);
        if (futbolista == null)
            return NotFound();

        return Ok(ConEquipo(futbolista));
    }

    [HttpPost]
    public IActionResult Agregar(CrearFutbolistaRequest request)
    {
        if (request.FechaNacimiento == default || request.FechaNacimiento > DateOnly.FromDateTime(DateTime.UtcNow))
            return BadRequest(new { error = "La fecha de nacimiento debe ser válida y no futura." });

        var futbolista = new Futbolista
        {
            Nombre = request.Nombre.Trim(),
            Apellido = request.Apellido.Trim(),
            Apodo = request.Apodo?.Trim(),
            Precio = request.Precio,
            FechaNacimiento = request.FechaNacimiento,
            IdEquipo = request.IdEquipo,
            Posicion = new Posicion { IdPosicion = request.IdPosicion }
        };

        try
        {
            var nuevoFutbolista = service.Agregar(futbolista);
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
        if (request.Precio <= 0)
            return BadRequest(new { error = "La cotización debe ser mayor que cero." });

        var existente = service.ObtenerPorId(id);
        if (existente == null)
            return NotFound();

        existente.Precio = request.Precio;
        existente.IdEquipo = request.IdEquipo;

        try
        {
            service.Actualizar(existente);
            var actualizado = service.ObtenerPorId(id);
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
        if (!service.Eliminar(id))
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
