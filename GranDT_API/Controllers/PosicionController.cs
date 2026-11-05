using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_Clases.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PosicionController : ControllerBase
{
    private readonly PosicionService service;

    public PosicionController(PosicionService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<Posicion>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Posicion> ObtenerPorId(int id)
    {
        var posicion = service.ObtenerPorId(id);

        if (posicion == null)
            return NotFound();

        return Ok(posicion);
    }

    [HttpPost]
    public IActionResult Agregar(CrearPosicionRequest request)
    {
        try
        {
            var nuevaPosicion = service.Agregar(request.Nombre);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaPosicion.IdPosicion }, nuevaPosicion);
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