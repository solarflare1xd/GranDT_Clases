using GranDT_Clases.Servicios;

class BCryptEncriptacion : IEncriptacion
{
    public string Encriptar(string texto)
    {
        return BCrypt.Net.BCrypt.HashPassword(texto);
    }
}