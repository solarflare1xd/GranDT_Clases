using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql; // Tu namespace de conexión
using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories
{
    public class JugadorRepositoryMemoria: IJugadorRepository
    {
        private readonly Conexion _conexionBD;

        public JugadorRepositoryMemoria()
        {
            _conexionBD = new Conexion();
        }

        public List<Futbolista> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                var jugadores = new Dictionary<int, Futbolista>();

                db.Query<Futbolista, Posicion, Puntuacion, Futbolista>(
                    "sp_ObtenerTodosJugadores",
                    (jugador, posicion, puntuacion) =>
                    {
                        if (!jugadores.TryGetValue(jugador.IdJugador, out var existente))
                        {
                            existente = jugador;
                            existente.Posicion = posicion;
                            jugadores.Add(existente.IdJugador, existente);
                        }

                        if (puntuacion != null && !string.IsNullOrEmpty(puntuacion.IdPuntuacion))
                        {
                            existente.HistorialPuntajes.Add(puntuacion);
                        }

                        return existente;
                    },
                    splitOn: "IdPosicion,IdPuntuacion",
                    commandType: CommandType.StoredProcedure
                );

                return jugadores.Values.ToList();
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
                Futbolista? resultado = null;
                var puntuaciones = new HashSet<string>();

                db.Query<Futbolista, Posicion, Puntuacion, Futbolista>(
                    "sp_ObtenerJugadorPorId",
                    (jugador, posicion, puntuacion) =>
                    {
                        resultado ??= jugador;
                        resultado.Posicion = posicion;

                        if (puntuacion != null
                            && !string.IsNullOrEmpty(puntuacion.IdPuntuacion)
                            && puntuaciones.Add(puntuacion.IdPuntuacion))
                        {
                            resultado.HistorialPuntajes.Add(puntuacion);
                        }

                        return resultado;
                    },
                    new { p_IdJugador = id },
                    splitOn: "IdPosicion,IdPuntuacion",
                    commandType: CommandType.StoredProcedure
                );

                return resultado;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public List<Futbolista> ObtenerPorNombre(string nombre)
        {
            var db = _conexionBD.establecerconexion();

            try
            {
                var jugadores = new Dictionary<int, Futbolista>();

                db.Query<Futbolista, Posicion, Puntuacion, Futbolista>(
                    "sp_ObtenerJugadoresPorNombre",
                    (jugador, posicion, puntuacion) =>
                    {
                        if (!jugadores.TryGetValue(jugador.IdJugador, out var existente))
                        {
                            existente = jugador;
                            existente.Posicion = posicion;
                            jugadores.Add(existente.IdJugador, existente);
                        }

                        if (puntuacion != null && !string.IsNullOrEmpty(puntuacion.IdPuntuacion))
                        {
                            existente.HistorialPuntajes.Add(puntuacion);
                        }

                        return existente;
                    },
                    new { p_Nombre = nombre },
                    splitOn: "IdPosicion,IdPuntuacion",
                    commandType: CommandType.StoredProcedure
                );

                return jugadores.Values.ToList();
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
                if (jugador.Posicion == null)
                {
                    throw new ArgumentException("El jugador debe tener una posición.", nameof(jugador));
                }

                var parametros = new DynamicParameters();
                parametros.Add("p_Nombre", jugador.Nombre);
                parametros.Add("p_Apellido", jugador.Apellido);
                parametros.Add("p_Apodo", jugador.Apodo);
                parametros.Add("p_Precio", jugador.Precio);
                parametros.Add("p_FechaNacimiento", jugador.FechaNacimiento.ToDateTime(TimeOnly.MinValue), DbType.Date);
                parametros.Add("p_IdPosicion", jugador.Posicion.IdPosicion);
                parametros.Add("p_IdEquipo", jugador.IdEquipo);

                int id = db.ExecuteScalar<int>(
                    "sp_AgregarJugadorConEquipo",
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