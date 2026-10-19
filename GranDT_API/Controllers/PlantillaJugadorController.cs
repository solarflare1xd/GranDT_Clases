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
    private readonly PlantillaService plantillaService;
    private readonly JugadorService jugadorService;

    public PlantillaJugadorController(
        PlantillaJugadorService service,
        PlantillaService plantillaService,
        JugadorService jugadorService)
    {
        this.service = service;
        this.plantillaService = plantillaService;
        this.jugadorService = jugadorService;
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
        if (request.IdPlantilla <= 0)
            return BadRequest(new { error = "El identificador de plantilla debe ser mayor que cero." });

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

    [HttpPost("/api/plantillas/{id:int}/jugadores")]
    public IActionResult AgregarAPlantilla(int id, AgregarJugadorAPlantillaRequest request)
    {
        var plantilla = plantillaService.ObtenerCompletaPorId(id);
        var futbolista = jugadorService.ObtenerPorId(request.IdJugador);
        if (plantilla == null || futbolista == null)
            return NotFound();

        if (service.ObtenerPorId(id, request.IdJugador) != null)
            return Conflict(new { error = "El futbolista ya pertenece a la plantilla." });

        var plantillaJugador = new PlantillaJugador
        {
            IdPlantilla = id,
            IdJugador = request.IdJugador,
            Numero = request.Numero,
            EsSuplente = request.EsSuplente
        };

        try
        {
            plantillaService.ValidarAgregarJugador(plantilla, futbolista, request.EsSuplente);
            service.Agregar(plantillaJugador);
            var actualizada = plantillaService.ObtenerCompletaPorId(id);
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