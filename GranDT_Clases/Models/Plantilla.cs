using System;
using System.Collections.Generic;
using System.Text;

namespace GranDT_Clases
{
    public class Plantilla
    {
        public string IdPlantilla { get; set; }
        public float Presupuesto { get; set; }
        public Futbolista[] Jugadores = new Futbolista[10];
        public Futbolista[] Jugadores_sup = new Futbolista[10];


    }

}
