using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    internal class Usuario
    {
        string Nombre { get; set; }
        string Apellido { get; set; }
        string Email { get; set; }
        DateOnly Nacimiento { get; set; }
        string Password { get; set; }

    }
}
