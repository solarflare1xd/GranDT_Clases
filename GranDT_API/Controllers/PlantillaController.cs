using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_Clases.Services;
using GranDT_Clases.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly PlantillaService service;
    private readonly UsuarioService usuarioService;

    public PlantillaController(PlantillaService service, UsuarioService usuarioService)
    {
        this.service = service;
        this.usuarioService = usuarioService;
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
    public IActionResult Agregar(CrearPlantillaRequest request)
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
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpGet("/api/usuarios/{id}/plantillas")]
    public ActionResult<List<Plantilla>> ObtenerPlantillasDeUsuario(string id)
    {
        if (usuarioService.ObtenerPor(id) == null)
            return NotFound();

        var plantilla = service.ObtenerPorUsuario(id);
        return Ok(plantilla == null ? new List<Plantilla>() : new List<Plantilla> { plantilla });
    }

    [HttpPost("/api/usuarios/{id}/plantillas")]
    public IActionResult CrearParaUsuario(string id, CrearPlantillaRequest request)
    {
        if (usuarioService.ObtenerPor(id) == null)
            return NotFound();

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

        try
        {
            var nuevaPlantilla = service.CrearCompleta(id, request.Presupuesto, integrantes);
            var respuesta = service.ObtenerCompletaPorId(nuevaPlantilla.IdPlantilla) ?? nuevaPlantilla;
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPlantilla.IdPlantilla }, respuesta);
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
}