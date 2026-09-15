using System;
using System.Collections.Generic;
using System.Text;

namespace GranDT_Clases
{
    public class Equipo
    {
        public string Nombre { get; set; }
        public List<Futbolista> Jugadores { get; set; } = new List<Futbolista>();

        
    }
}
