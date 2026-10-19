using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using GranDT_Clases;

namespace GranDT_api.Controllers;

internal static class ApiErrors
{
    public static bool IsKnown(Exception error) =>
        error is ArgumentException
        or KeyNotFoundException
        or ResourceConflictException
        || error is MySqlException mysqlError && mysqlError.Number is 1062 or 1452 or 1644
        || error.GetType() == typeof(Exception)
            && error.Message is "Datos inválidos" or "El usuario ya existe";

    public static IActionResult ToActionResult(Exception error) =>
        error switch
        {
            ResourceConflictException => new ConflictObjectResult(new { error = error.Message }),
            KeyNotFoundException => new NotFoundObjectResult(new { error = error.Message }),
            ArgumentException => new BadRequestObjectResult(new { error = error.Message }),
            Exception { Message: "Datos inválidos" } => new BadRequestObjectResult(new { error = error.Message }),
            Exception { Message: "El usuario ya existe" } => new ConflictObjectResult(new { error = error.Message }),
            MySqlException { Number: 1062 } => new ConflictObjectResult(new { error = "El recurso ya existe." }),
            MySqlException { Number: 1452 } => new NotFoundObjectResult(new { error = "No existe uno de los recursos referenciados." }),
            MySqlException { Number: 1644 } => new BadRequestObjectResult(new { error = error.Message }),
            _ => throw new ArgumentOutOfRangeException(nameof(error), "El error no tiene una respuesta HTTP definida.")
        };
}
