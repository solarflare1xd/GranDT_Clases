using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_api.Contracts.Responses;
using GranDT_Clases.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService service;

    public UsuarioController(UsuarioService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<UsuarioResponse>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos().Select(UsuarioResponse.Desde).ToList());
    }

    [HttpGet("{email}")]
    public ActionResult<UsuarioResponse> ObtenerPorEmail(string email)
    {
        var usuario = service.ObtenerPor(email);

        if (usuario == null)
            return NotFound();

        return Ok(UsuarioResponse.Desde(usuario));
    }

    [HttpPost]
    public IActionResult Agregar(CrearUsuarioRequest request)
    {
        if (request.Nacimiento == default || request.Nacimiento > DateOnly.FromDateTime(DateTime.UtcNow))
            return BadRequest(new { error = "La fecha de nacimiento debe ser válida y no futura." });

        var usuario = new Usuario
        {
            Nombre = request.Nombre.Trim(),
            Apellido = request.Apellido.Trim(),
            Email = request.Email.Trim(),
            Nacimiento = request.Nacimiento,
            Password = request.Password
        };

        if (service.ObtenerPor(usuario.Email) != null)
            return Conflict(new { error = "El usuario ya existe." });

        try
        {
            var nuevoUsuario = service.Agregar(usuario);
            var response = UsuarioResponse.Desde(nuevoUsuario);
            return CreatedAtAction(nameof(ObtenerPorEmail), new { email = nuevoUsuario.Email }, response);
        }
        catch (Exception error) when (ApiErrors.IsKnown(error))
        {
            return ApiErrors.ToActionResult(error);
        }
    }

    [HttpDelete("{email}")]
    public IActionResult Eliminar(string email)
    {
        if (!service.Eliminar(email))
            return NotFound();

        return NoContent();
    }
}