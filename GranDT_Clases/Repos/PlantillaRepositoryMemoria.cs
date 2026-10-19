using System.Collections.Generic;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql; // Tu namespace de conexión
using GranDT_Clases;
using GranDT_Clases.IRepos;
using System.Data;

namespace GranDT_Clases.Repositories
{
    public class PlantillaRepositoryMemoria : IPlantillaRepository
    {
        private readonly Conexion _conexionBD;

        public PlantillaRepositoryMemoria()
        {
            _conexionBD = new Conexion();
        }

        public List<Plantilla> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                var plantillas = db.Query<Plantilla>(
                    "sp_ObtenerTodasPlantillas",
                    commandType: CommandType.StoredProcedure
                ).ToList();
                return plantillas;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Plantilla? ObtenerPorId(int id)
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

        public Plantilla? ObtenerCompletaPorId(int id)
        {
            var db = _conexionBD.establecerconexion();

            try
            {
                var plantilla = db.QueryFirstOrDefault<Plantilla>(
                    "sp_ObtenerPlantillaPorId",
                    new { p_IdPlantilla = id },
                    commandType: CommandType.StoredProcedure);

                if (plantilla == null)
                    return null;

                var integrantes = db.Query<PlantillaJugador, Futbolista, Posicion, PlantillaJugador>(
                    """
                    SELECT
                        pj.IdPlantilla, pj.IdJugador, pj.Numero, pj.EsSuplente,
                        j.IdJugador AS JugadorId, j.Nombre, j.Apellido, j.Apodo, j.Precio,
                        j.FechaNacimiento, j.IdEquipo,
                        pos.IdPosicion, pos.Nombre
                    FROM PlantillaJugador pj
                    INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
                    LEFT JOIN Posicion pos ON pos.IdPosicion = j.IdPosicion
                    WHERE pj.IdPlantilla = @IdPlantilla
                    """,
                    (integrante, jugador, posicion) =>
                    {
                        jugador.IdJugador = integrante.IdJugador;
                        jugador.Posicion = posicion;
                        integrante.Futbolista = jugador;
                        return integrante;
                    },
                    new { IdPlantilla = id },
                    splitOn: "JugadorId,IdPosicion").ToList();

                foreach (var integrante in integrantes)
                {
                    if (integrante.Futbolista == null)
                        continue;

                    if (integrante.EsSuplente)
                        plantilla.Jugadores_sup.Add(integrante.Futbolista);
                    else
                        plantilla.Jugadores.Add(integrante.Futbolista);
                }

                return plantilla;
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
                plantilla.IdPlantilla = db.QuerySingle<int>(
                    "sp_AgregarPlantilla",
                    new 
                    { 
                        p_Presupuesto = plantilla.Presupuesto
                    },
                    commandType: CommandType.StoredProcedure
                );

                return plantilla;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Plantilla CrearCompleta(Plantilla plantilla, IReadOnlyCollection<PlantillaJugador> integrantes)
        {
            var db = _conexionBD.establecerconexion();
            try
            {
                using var transaccion = db.BeginTransaction();
                plantilla.IdPlantilla = db.QuerySingle<int>(
                    "sp_AgregarPlantilla",
                    new { p_Presupuesto = plantilla.Presupuesto },
                    transaccion,
                    commandType: CommandType.StoredProcedure);

                foreach (var integrante in integrantes)
                {
                    integrante.IdPlantilla = plantilla.IdPlantilla;
                    db.Execute(
                        "sp_AgregarPlantillaJugador",
                        new
                        {
                            p_IdPlantilla = plantilla.IdPlantilla,
                            p_IdJugador = integrante.IdJugador,
                            p_Numero = integrante.Numero,
                            p_EsSuplente = integrante.EsSuplente
                        },
                        transaccion,
                        commandType: CommandType.StoredProcedure);
                }

                var titulares = db.QuerySingle<int>(
                    """
                    SELECT COUNT(*)
                    FROM PlantillaJugador
                    WHERE IdPlantilla = @IdPlantilla AND EsSuplente = 0
                    """,
                    new { plantilla.IdPlantilla },
                    transaccion);

                if (titulares == 11)
                {
                    db.Execute(
                        "sp_ValidarPlantilla",
                        new { p_IdPlantilla = plantilla.IdPlantilla },
                        transaccion,
                        commandType: CommandType.StoredProcedure);
                }

                transaccion.Commit();
                return plantilla;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Plantilla CrearCompleta(string email, Plantilla plantilla, IReadOnlyCollection<PlantillaJugador> integrantes)
        {
            var db = _conexionBD.establecerconexion();

            try
            {
                using var transaccion = db.BeginTransaction();
                plantilla.IdPlantilla = db.QuerySingle<int>(
                    "sp_AgregarPlantilla",
                    new { p_Presupuesto = plantilla.Presupuesto },
                    transaccion,
                    commandType: CommandType.StoredProcedure);

                foreach (var integrante in integrantes)
                {
                    integrante.IdPlantilla = plantilla.IdPlantilla;
                    db.Execute(
                        "sp_AgregarPlantillaJugador",
                        new
                        {
                            p_IdPlantilla = plantilla.IdPlantilla,
                            p_IdJugador = integrante.IdJugador,
                            p_Numero = integrante.Numero,
                            p_EsSuplente = integrante.EsSuplente
                        },
                        transaccion,
                        commandType: CommandType.StoredProcedure);
                }

                var filasUsuario = db.Execute(
                    """
                    UPDATE Usuario
                    SET IdPlantilla = @IdPlantilla
                    WHERE Email = @Email AND IdPlantilla IS NULL
                    """,
                    new { IdPlantilla = plantilla.IdPlantilla, Email = email },
                    transaccion);

                if (filasUsuario == 0)
                {
                    var existeUsuario = db.QuerySingle<int>(
                        "SELECT COUNT(*) FROM Usuario WHERE Email = @Email",
                        new { Email = email },
                        transaccion);
                    if (existeUsuario == 0)
                        throw new KeyNotFoundException($"No existe el usuario {email}.");
                    throw new ResourceConflictException("El usuario ya tiene una plantilla.");
                }

                var cantidadTitulares = db.QuerySingle<int>(
                    """
                    SELECT COUNT(*)
                    FROM PlantillaJugador
                    WHERE IdPlantilla = @IdPlantilla AND EsSuplente = 0
                    """,
                    new { plantilla.IdPlantilla },
                    transaccion);

                if (cantidadTitulares == 11)
                {
                    db.Execute(
                        "sp_ValidarPlantilla",
                        new { p_IdPlantilla = plantilla.IdPlantilla },
                        transaccion,
                        commandType: CommandType.StoredProcedure);
                }

                transaccion.Commit();
                return plantilla;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Plantilla? ObtenerPorUsuario(string email)
        {
            var db = _conexionBD.establecerconexion();

            try
            {
                using var resultados = db.QueryMultiple(
                    "sp_ObtenerPlantillaPorUsuario",
                    new { p_Email = email },
                    commandType: CommandType.StoredProcedure);

                var plantilla = resultados.ReadFirstOrDefault<Plantilla>();
                if (plantilla == null)
                {
                    return null;
                }

                var miembros = resultados.Read<PlantillaJugador, Futbolista, Posicion, PlantillaJugador>(
                    (miembro, jugador, posicion) =>
                    {
                        miembro.IdJugador = jugador.IdJugador;
                        jugador.Posicion = posicion;
                        miembro.Futbolista = jugador;
                        return miembro;
                    },
                    splitOn: "IdJugador,IdPosicion").ToList();

                var puntuaciones = resultados.Read<Puntuacion>().ToList();
                var jugadores = miembros
                    .Where(miembro => miembro.Futbolista != null)
                    .ToDictionary(miembro => miembro.IdJugador, miembro => miembro.Futbolista!);

                foreach (var puntuacion in puntuaciones)
                {
                    if (jugadores.TryGetValue(puntuacion.IdJugador, out var jugador))
                    {
                        jugador.HistorialPuntajes.Add(puntuacion);
                    }
                }

                foreach (var miembro in miembros)
                {
                    if (miembro.Futbolista == null)
                    {
                        continue;
                    }

                    if (miembro.EsSuplente)
                    {
                        plantilla.Jugadores_sup.Add(miembro.Futbolista);
                    }
                    else
                    {
                        plantilla.Jugadores.Add(miembro.Futbolista);
                    }
                }

                return plantilla;
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