using BibliotecaApi.Models; // Cambiado a plural para coincidir
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
    public ActionResult<Jugador> ObtenerPorId(int id) // Corregido de Libro a Jugador
    {
        var jugador = service.ObtenerPorId(id); // Corregido a minúscula o como prefieras

        if (jugador == null)
            return NotFound();

        return Ok(jugador); // Corregido de libro a jugador
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