using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql;
using GranDT_Clases;
using GranDT_Clases.IRepos;
using GranDT_Clases.Models;

namespace GranDT_Clases.Repositories
{
    public class PlantillaJugadorRepositoryDapper : IPlantillaJugadorRepository
    {
        private readonly Conexion _conexionBD;

        public PlantillaJugadorRepositoryDapper()
        {
            _conexionBD = new Conexion();
        }

        public List<PlantillaJugador> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.Query<PlantillaJugador>(
                    "sp_ObtenerTodasPlantillaJugadores",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public PlantillaJugador? ObtenerPorId(string idPlantilla, int idJugador)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.QueryFirstOrDefault<PlantillaJugador>(
                    "sp_ObtenerPlantillaJugadorPorId",
                    new 
                    { 
                        p_IdPlantilla = idPlantilla, 
                        p_IdJugador = idJugador 
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public PlantillaJugador Agregar(PlantillaJugador plantillaJugador)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                db.Execute(
                    "sp_AgregarPlantillaJugador",
                    new 
                    { 
                        p_IdPlantilla = plantillaJugador.IdPlantilla,
                        p_IdJugador = plantillaJugador.IdJugador,
                        p_Numero = plantillaJugador.Numero,
                        p_EsSuplente = plantillaJugador.EsSuplente
                    },
                    commandType: CommandType.StoredProcedure
                );

                return plantillaJugador;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public bool Eliminar(string idPlantilla, int idJugador)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                int filasAfectadas = db.Execute(
                    "sp_EliminarPlantillaJugador",
                    new 
                    { 
                        p_IdPlantilla = idPlantilla, 
                        p_IdJugador = idJugador 
                    },
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