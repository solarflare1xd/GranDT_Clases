using System;

namespace GranDT_Clases
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateOnly Nacimiento { get; set; }
        public string Password { get; set; }
        
        public bool EsAdministrador { get; set; }

        public Plantilla? PlantillaUsuario { get; set; }
    }
}