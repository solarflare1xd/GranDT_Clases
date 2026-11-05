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
    public IActionResult Agregar(CrearPlantillaRequest request)
    {
        try
        {
            var nuevaPlantilla = service.CrearCompleta(
                request.Presupuesto,
                request.Titulares ?? new List<int>(),
                request.Suplentes ?? new List<int>());
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
        var plantillas = service.ObtenerTodasDeUsuario(id);
        if (plantillas == null)
            return NotFound();

        return Ok(plantillas);
    }

    [HttpPost("/api/usuarios/{id}/plantillas")]
    public IActionResult CrearParaUsuario(string id, CrearPlantillaRequest request)
    {
        try
        {
            var nuevaPlantilla = service.CrearCompletaParaUsuario(
                id,
                request.Presupuesto,
                request.Titulares ?? new List<int>(),
                request.Suplentes ?? new List<int>());
            if (nuevaPlantilla == null)
                return NotFound();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPlantilla.IdPlantilla }, nuevaPlantilla);
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
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