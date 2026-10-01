using Dapper;
using MySql.Data.MySqlClient;

namespace Mundial2026_wazaaaaa.Clases_sql
{
    internal class Conexion
    {
        private static readonly string servidor = "localhost";
        private static readonly string bd = "GranDT";
        private static readonly string usuario = "pepito";
        private static readonly string password = "";
        private static readonly string puerto = "3306";

        private readonly string cadenaConexion = Environment.GetEnvironmentVariable("GRANDT_TEST_CONNECTION_STRING")
            ?? $"server={servidor};database={bd};uid={usuario};pwd={password};port={puerto};";

        
        private MySqlConnection conex;

        public MySqlConnection establecerconexion()
        {
            try
            {
                if (conex == null)
                {
                    conex = new MySqlConnection(cadenaConexion);
                }

                if (conex.State != System.Data.ConnectionState.Open)
                {
                    conex.Open();
                }

                return conex;
            }
            catch (MySqlException)
            {
                return null;
            }
        }

        public void cerrarConexion()
        {
            if (conex != null && conex.State == System.Data.ConnectionState.Open)
            {
                conex.Close();
            }
        }

    }
}