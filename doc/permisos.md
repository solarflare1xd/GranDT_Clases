# Permisos de usuarios MySQL

El script `05 Usuarios.sql` crea dos cuentas de MySQL para la base `GranDT`:

| Cuenta | Permisos sobre `GranDT.*` |
| --- | --- |
| `grandt_app` | Todos los privilegios de la base del proyecto |
| `Usuario` | Solo `SELECT` |

Estas son cuentas de base de datos; no representan roles de autenticación HTTP.

## Preparación y ejecución

1. Ejecutá primero `01 DDL Grandt.sql` para crear el esquema.
2. En `05 Usuarios.sql`, reemplazá los dos valores `REEMPLAZAR_CON_CLAVE_SEGURA_...` por contraseñas seguras y distintas. No guardes las contraseñas reales en Git.
3. Ejecutá el script con una cuenta administrativa de MySQL, por ejemplo desde el cliente:

```text
SOURCE C:/ruta/al/proyecto/doc/05 Usuarios.sql;
```

El script elimina permisos previos de estas dos cuentas antes de aplicar los nuevos, de modo que `Usuario` no conserve permisos de escritura concedidos anteriormente.

## Comprobación

Como administrador de MySQL, revisá los permisos concedidos:

```sql
SHOW GRANTS FOR 'grandt_app'@'localhost';
SHOW GRANTS FOR 'Usuario'@'localhost';
```

Conectate como `Usuario` y comprobá que la lectura funciona:

```sql
SELECT COUNT(*) FROM GranDT.Equipo;
```

Luego intentá una escritura; debe ser rechazada con un error de acceso denegado (normalmente `ERROR 1142`):

```sql
INSERT INTO GranDT.Equipo (Nombre) VALUES ('PRUEBA_PERMISO_INVITADO');
```

Registrá aquí la fecha, el resultado de la consulta `SELECT` y el error del `INSERT` después de ejecutar la comprobación en MySQL. No marques la verificación como completada hasta probarla con las cuentas configuradas.
