using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql; // Tu namespace donde está la clase Conexion
using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories
{
    public class PosicionRepositoryMemoria : IPosicionRepository
    {
        private readonly Conexion _conexionBD;

        public PosicionRepositoryMemoria()
        {
            _conexionBD = new Conexion();
        }

        public List<Posicion> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.Query<Posicion>(
                    "sp_ObtenerTodasPosiciones",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Posicion? ObtenerPorId(int id)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.QueryFirstOrDefault<Posicion>(
                    "sp_ObtenerPosicionPorId",
                    new { p_IdPosicion = id },
                    commandType: CommandType.StoredProcedure
                );
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Posicion Agregar(Posicion posicion)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                // ExecuteScalar ejecuta el INSERT y devuelve la primera columna de la primera fila (el LAST_INSERT_ID)
                int idGenerado = db.ExecuteScalar<int>(
                    "sp_AgregarPosicion",
                    new { p_Nombre = posicion.Nombre }, // Ajusta "Nombre" si tu propiedad se llama distinto
                    commandType: CommandType.StoredProcedure
                );

                // Asignamos el ID generado por la base de datos al objeto
                posicion.IdPosicion = idGenerado;
                
                return posicion;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public bool Eliminar(int id)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                int filasAfectadas = db.Execute(
                    "sp_EliminarPosicion",
                    new { p_IdPosicion = id },
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