using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql; // Tu namespace de conexión
using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories
{
    // Mantengo el nombre que me pediste
    public class PuntuacionRepositoryMemoria : IPuntuacionRepository
    {
        private readonly Conexion _conexionBD;

        public PuntuacionRepositoryMemoria()
        {
            _conexionBD = new Conexion();
        }

        public List<Puntuacion> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.Query<Puntuacion>(
                    "sp_ObtenerTodasPuntuaciones",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Puntuacion? ObtenerPorId(string id)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.QueryFirstOrDefault<Puntuacion>(
                    "sp_ObtenerPuntuacionPorId",
                    new { p_IdPuntuacion = id },
                    commandType: CommandType.StoredProcedure
                );
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        // ATENCIÓN ACÁ: 
        // Como el objeto Puntuacion no tiene el IdJugador, tenés que pedirlo como parámetro en el método.
        // Vas a tener que actualizar la interfaz IPuntuacionRepository para que coincida.
        public Puntuacion Agregar(Puntuacion puntuacion, int idJugador)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                db.Execute(
                    "sp_AgregarPuntuacion",
                    new 
                    { 
                        p_IdPuntuacion = puntuacion.IdPuntuacion,
                        p_Partido_date = puntuacion.Partido_date,
                        p_Puntaje = puntuacion.Puntaje,
                        p_IdJugador = idJugador // Lo sacamos del parámetro extra, no del objeto
                    },
                    commandType: CommandType.StoredProcedure
                );

                return puntuacion;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public bool Eliminar(string id)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                int filasAfectadas = db.Execute(
                    "sp_EliminarPuntuacion",
                    new { p_IdPuntuacion = id },
                    commandType: CommandType.StoredProcedure
                );

                return filasAfectadas > 0;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }
    }
}