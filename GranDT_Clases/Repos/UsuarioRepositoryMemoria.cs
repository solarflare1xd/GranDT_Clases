using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Mundial2026_wazaaaaa.Clases_sql; // Tu namespace donde está la clase Conexion
using GranDT_Clases;
using GranDT_Clases.IRepos;

namespace GranDT_Clases.Repositories
{
    public class UsuarioRepositoryMemoria : IUsuarioRepository
    {
        private readonly Conexion _conexionBD;

        public UsuarioRepositoryMemoria()
        {
            _conexionBD = new Conexion();
        }

        public List<Usuario> ObtenerTodos()
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.Query<Usuario>(
                    "sp_ObtenerTodosUsuarios",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                return db.QueryFirstOrDefault<Usuario>(
                    "sp_ObtenerUsuarioPorEmail",
                    new { p_Email = email },
                    commandType: CommandType.StoredProcedure
                );
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public Usuario Agregar(Usuario usuario)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                string? idPlantilla = usuario.PlantillaUsuario != null ? usuario.PlantillaUsuario.IdPlantilla : null;

                db.Execute(
                    "sp_AgregarUsuario",
                    new 
                    { 
                        p_Email = usuario.Email,
                        p_Nombre = usuario.Nombre,
                        p_Apellido = usuario.Apellido,
                        p_Nacimiento = usuario.Nacimiento,
                        p_Password = usuario.Password,
                        p_EsAdministrador = usuario.EsAdministrador,
                        p_IdPlantilla = idPlantilla
                    },
                    commandType: CommandType.StoredProcedure
                );

                return usuario;
            }
            finally
            {
                _conexionBD.cerrarConexion();
            }
        }

        public bool Eliminar(string email)
        {
            var db = _conexionBD.establecerconexion();
            
            try
            {
                int filasAfectadas = db.Execute(
                    "sp_EliminarUsuario",
                    new { p_Email = email },
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