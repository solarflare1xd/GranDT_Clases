using GranDT_Clases;
using GranDT_api.Contracts.Requests;
using GranDT_api.Contracts.Responses;
using GranDT_Clases.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public ActionResult<UsuarioResponse> Agregar(CrearUsuarioRequest request)
    {
        var usuario = new Usuario
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Email = request.Email,
            Nacimiento = request.Nacimiento,
            Password = request.Password,
            EsAdministrador = request.EsAdministrador
        };
        var nuevoUsuario = service.Agregar(usuario);

        var response = UsuarioResponse.Desde(nuevoUsuario);
        return CreatedAtAction(nameof(ObtenerPorEmail), new { email = nuevoUsuario.Email }, response);
    }

    [HttpDelete("{email}")]
    public IActionResult Eliminar(string email)
    {
        if (!service.Eliminar(email))
            return NotFound();

        return NoContent();
    }
}