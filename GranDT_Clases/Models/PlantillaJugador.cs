namespace GranDT_Clases
{
    public class PlantillaJugador
    {
        public int IdPlantilla { get; set; }
        public int IdJugador { get; set; }
        public int Numero { get; set; }
        public bool EsSuplente { get; set; }
        public Futbolista? Futbolista { get; set; }
    }
}