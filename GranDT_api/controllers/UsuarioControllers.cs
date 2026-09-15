using System.Collections.Generic;
using GranDT_Clases;
using GranDT_api.Services;
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
    public ActionResult<List<Usuario>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{email}")]
    public ActionResult<Usuario> ObtenerPorEmail(string email)
    {
        var usuario = service.ObtenerPorId(email);

        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    public ActionResult<Usuario> Agregar(Usuario usuario)
    {
        var nuevoUsuario = service.Agregar(usuario);

        return Ok(nuevoUsuario);
    }

    [HttpDelete("{email}")]
    public IActionResult Eliminar(string email)
    {
        var eliminado = service.Eliminar(email);

        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}

