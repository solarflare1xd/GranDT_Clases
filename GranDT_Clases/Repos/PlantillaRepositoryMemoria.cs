using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql; // Tu namespace de conexión
using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories
{
    public class PlantillaRepositoryDapper : IPlantillaRepository
    {
        private readonly Conexion _conexionBD;

        public PlantillaRepositoryDapper()
        {
            _conexionBD = new Conexion();
        }

        public List<Plantilla> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.Query<Plantilla>(
                    "sp_ObtenerTodasPlantillas",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Plantilla? ObtenerPorId(string id)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.QueryFirstOrDefault<Plantilla>(
                    "sp_ObtenerPlantillaPorId",
                    new { p_IdPlantilla = id },
                    commandType: CommandType.StoredProcedure
                );
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Plantilla Agregar(Plantilla plantilla)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                db.Execute(
                    "sp_AgregarPlantilla",
                    new 
                    { 
                        p_IdPlantilla = plantilla.IdPlantilla,
                        p_Presupuesto = plantilla.Presupuesto
                    },
                    commandType: CommandType.StoredProcedure
                );

                // Como el Id es un string que ya viene en el objeto, 
                // solo retornamos la misma entidad.
                return plantilla;
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
                    "sp_EliminarPlantilla",
                    new { p_IdPlantilla = id },
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