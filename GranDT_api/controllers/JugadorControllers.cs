using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JugadorController : ControllerBase
{
    private readonly JugadorService service;

    public JugadorController(JugadorService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult<List<Jugador>> ObtenerTodos()
    {
        return Ok(service.ObtenerTodos());
    }

    [HttpGet("{id}")]
    public ActionResult<Libro> ObtenerPorId(int id)
    {
        var Jugador = service.ObtenerPorId(id);

        if (Jugador == null)
            return NotFound();

        return Ok(libro);
    }

    [HttpPost]
    public ActionResult<Jugador> Agregar(Jugador jugador)
    {
        var nuevoJugador = service.Agregar(jugador);

        return Ok(nuevoJugador);
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var eliminado = service.Eliminar(id);

        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}

