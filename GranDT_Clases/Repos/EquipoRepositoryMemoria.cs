using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql;
using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories
{
    public class EquipoRepositoryDapper : IEquipoRepository
    {
        private readonly Conexion _conexionBD;

        public EquipoRepositoryDapper()
        {
            
            _conexionBD = new Conexion();
        }

        public List<Equipo> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                var equipoDictionary = new Dictionary<string, Equipo>();

                db.Query<Equipo, Futbolista, Equipo>(
                    "sp_ObtenerTodosEquipos",
                    (equipo, jugador) =>
                    {
                        if (!equipoDictionary.TryGetValue(equipo.Nombre, out var equipoEntry))
                        {
                            equipoEntry = equipo;
                            equipoEntry.Jugadores = new List<Futbolista>();
                            equipoDictionary.Add(equipoEntry.Nombre, equipoEntry);
                        }

                        if (jugador != null && jugador.IdJugador != 0)
                        {
                            equipoEntry.Jugadores.Add(jugador);
                        }

                        return equipoEntry;
                    },
                    splitOn: "IdJugador",
                        commandType: CommandType.StoredProcedure
                );

                return equipoDictionary.Values.ToList();
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Equipo? ObtenerPorNombre(string nombre)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                Equipo? equipoResult = null;

                db.Query<Equipo, Futbolista, Equipo>(
                    "sp_ObtenerEquipoPorNombre",
                    (equipo, jugador) =>
                    {
                        if (equipoResult == null)
                        {
                            equipoResult = equipo;
                            equipoResult.Jugadores = new List<Futbolista>();
                        }

                        if (jugador != null && jugador.IdJugador != 0)
                        {
                            equipoResult.Jugadores.Add(jugador);
                        }

                        return equipoResult;
                    },
                    new { p_Nombre = nombre },
                    splitOn: "IdJugador",
                    commandType: CommandType.StoredProcedure
                );

                return equipoResult;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Equipo Agregar(Equipo equipo)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                db.Execute(
                    "sp_AgregarEquipo",
                    new { p_Nombre = equipo.Nombre },
                    commandType: CommandType.StoredProcedure
                );

                return equipo;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public bool Eliminar(string nombre)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                int filasAfectadas = db.Execute(
                    "sp_EliminarEquipo",
                    new { p_Nombre = nombre },
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