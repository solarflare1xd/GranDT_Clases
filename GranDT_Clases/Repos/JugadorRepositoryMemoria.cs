using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql; // Tu namespace de conexión
using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories
{
    public class JugadorRepositoryDapper : IJugadorRepository
    {
        private readonly Conexion _conexionBD;

        public JugadorRepositoryDapper()
        {
            _conexionBD = new Conexion();
        }

        public List<Futbolista> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.Query<Futbolista>(
                    "sp_ObtenerTodosJugadores",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Futbolista? ObtenerPorId(int id)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.QueryFirstOrDefault<Futbolista>(
                    "sp_ObtenerJugadorPorId",
                    new { p_IdJugador = id },
                    commandType: CommandType.StoredProcedure
                );
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Futbolista Agregar(Futbolista jugador)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                // Mapeamos los parámetros, pasando el Enum como texto
                var parametros = new
                {
                    p_Nombre = jugador.Nombre,
                    p_Apellido = jugador.Apellido,
                    p_Apodo = jugador.Apodo,
                    p_Precio = jugador.Precio,
                    p_FechaNacimiento = jugador.FechaNacimiento, // Soportado nativamente por MySqlConnector
                    p_Posicion = jugador.Posicion.ToString() 
                };

                // ExecuteScalar ejecuta el INSERT y nos devuelve el LAST_INSERT_ID()
                int id = db.ExecuteScalar<int>(
                    "sp_AgregarJugador",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                jugador.IdJugador = id;
                return jugador;
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
                    "sp_EliminarJugador",
                    new { p_IdJugador = id },
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