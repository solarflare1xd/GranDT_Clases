using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Mundial2026_wazaaaaa.Clases_sql;

namespace GranDT_api.Tests;

public abstract class TestRepo : IDisposable
{
    private static int _secuenciaPruebas;
    private readonly MySqlTransaction _transaccion;
    private readonly Conexion _configuracionConexion;
    protected readonly MySqlConnection _conexion;

    protected TestRepo()
    {
        _configuracionConexion = new Conexion();
        _conexion = _configuracionConexion.establecerconexion();
        _transaccion = _conexion.BeginTransaction();
    }

    protected void Ejecutar(string sql, object? parametros = null, CommandType tipo = CommandType.StoredProcedure)
    {
        _conexion.Execute(sql, parametros, _transaccion, commandType: tipo);
    }

    protected List<T> Consultar<T>(string sql, object? parametros = null, CommandType tipo = CommandType.StoredProcedure)
    {
        return _conexion.Query<T>(sql, parametros, _transaccion, commandType: tipo).AsList();
    }

    protected T ConsultarUno<T>(string sql, object? parametros = null, CommandType tipo = CommandType.StoredProcedure)
    {
        return _conexion.QuerySingle<T>(sql, parametros, _transaccion, commandType: tipo);
    }

    protected T? ConsultarOpcional<T>(string sql, object? parametros = null, CommandType tipo = CommandType.StoredProcedure)
    {
        return _conexion.QuerySingleOrDefault<T>(sql, parametros, _transaccion, commandType: tipo);
    }

    protected static string NuevoIdPrueba()
    {
        return Interlocked.Increment(ref _secuenciaPruebas).ToString();
    }

    protected int CrearPosicion()
    {
        return ConsultarUno<int>("sp_AgregarPosicion", new { p_Nombre = NuevoIdPrueba() });
    }

    protected int CrearJugador(int idPosicion)
    {
        return ConsultarUno<int>(
            "sp_AgregarJugador",
            new
            {
                p_Nombre = "Integration",
                p_Apellido = "Test",
                p_Apodo = NuevoIdPrueba(),
                p_Precio = 1.25,
                p_FechaNacimiento = new DateTime(2000, 1, 1),
                p_IdPosicion = idPosicion
            });
    }

    public void Dispose()
    {
        _transaccion.Rollback();
        _transaccion.Dispose();
        _configuracionConexion.cerrarConexion();
    }
}
