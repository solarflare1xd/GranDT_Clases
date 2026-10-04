namespace GranDT_Clases
{
    public class PlantillaJugador
    {
        public string IdPlantilla { get; set; }
        public int IdJugador { get; set; }
        public int Numero { get; set; }
        public bool EsSuplente { get; set; }
        public Futbolista? Futbolista { get; set; }
    }
}