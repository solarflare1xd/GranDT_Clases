# Gran DT

Web API para administrar equipos, futbolistas, usuarios, plantillas y puntuaciones.

## Requisitos

- MySQL 8 en ejecución.
- .NET SDKs compatibles con los proyectos actuales: la API y la biblioteca apuntan a `net8.0`; el proyecto de pruebas apunta a `net10.0`.
- Cliente de MySQL (`mysql`) disponible en `PATH`.

## Preparar MySQL

Iniciá el servicio de MySQL 8 y abrí **Símbolo del sistema (CMD)** en la raíz del repositorio. Ejecutá los scripts en este orden:

```cmd
mysql -u root -p < "doc\01 DDL Grandt.sql"
mysql -u root -p < "doc\02 Triggers.sql"
mysql -u root -p < "doc\03 Procedures Grandt.sql"
mysql -u root -p < "doc\04 Inserts.sql"
```

Cada comando solicita la contraseña de la cuenta administrativa de MySQL. El primer script crea el esquema `GranDT` desde cero y reemplaza cualquier esquema existente con ese nombre.

Antes de crear las cuentas, abrí `doc\05 Usuarios.sql` y reemplazá los dos valores `REEMPLAZAR_CON_CLAVE_SEGURA_...` por contraseñas seguras distintas. No guardes contraseñas reales en Git. Después ejecutá:

```cmd
mysql -u root -p < "doc\05 Usuarios.sql"
```

El script crea `grandt_app` con todos los permisos sobre `GranDT.*` y `Usuario` con permiso de solo lectura. La comprobación de permisos está en [doc/permisos.md](doc/permisos.md).

Si necesitás actualizar una base existente que tenga una definición antigua de `Plantilla.Presupuesto`, revisá `doc\06 Migrar presupuesto.sql`; luego volvé a ejecutar los scripts de triggers y procedimientos. No ejecutes el DDL desde cero si necesitás conservar datos.

## Configurar y ejecutar la API

Desde PowerShell, en la raíz del repositorio, configurá la conexión en la sesión actual. Usá la contraseña que asignaste a `grandt_app`:

```powershell
$env:GRANDT_TEST_CONNECTION_STRING = "Server=localhost;Port=3306;Database=GranDT;User ID=grandt_app;Password=REEMPLAZAR_POR_TU_CLAVE;"
dotnet run --project .\GranDT_API\BibliotecaApi.csproj
```

Con el perfil de desarrollo, Scalar queda disponible en <http://localhost:5155/scalar/v1> y el documento OpenAPI en <http://localhost:5155/openapi/v1.json>. Mantené la variable de entorno configurada en la misma terminal donde ejecutás la API.

## Ejecutar las pruebas

Las pruebas de integración usan MySQL y el esquema, triggers y procedimientos preparados arriba. En otra sesión de PowerShell, configurá la misma cadena de conexión y ejecutá:

```powershell
$env:GRANDT_TEST_CONNECTION_STRING = "Server=localhost;Port=3306;Database=GranDT;User ID=grandt_app;Password=REEMPLAZAR_POR_TU_CLAVE;"
dotnet test .\TestClases.tests\TestClases.tests.csproj
```

Para restaurar paquetes antes de compilar o probar:

```powershell
dotnet restore .\GranDT_Clases.slnx
```

La variable de entorno evita usar la conexión de respaldo definida en el código. No guardes la cadena de conexión ni sus credenciales en archivos versionados.
