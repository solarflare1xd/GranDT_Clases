# GranDT

Biblioteca de dominio y API REST para administrar jugadores, equipos, usuarios, plantillas y puntuaciones de un torneo de futbol de fantasia.

## Requisitos

- .NET SDK 8
- MySQL 8.0
- Una base local de desarrollo o pruebas

## Preparar la base de datos

Ejecutar los scripts en este orden desde un cliente MySQL:

1. `doc/01 DDL Grandt.sql`
2. `doc/02 Triggers.sql`
3. `doc/03 Procedures Grandt.sql`
4. `doc/04 Inserts.sql` (opcional, carga datos de ejemplo)
5. `doc/05 Usuarios.sql` (opcional, crea el usuario de la aplicacion)

**Atencion:** el DDL elimina y vuelve a crear el esquema `GranDT`. No ejecutarlo sobre una base con datos que se quieran conservar.

El importe comun de presupuesto se configura inicialmente en `Configuracion` en `$99.999.999,99`, y el maximo de jugadores por plantilla se inicia en 20. El procedure `sp_ActualizarConfiguracion` permite cambiar ambos valores si las plantillas existentes siguen siendo validas.

El usuario de `05 Usuarios.sql` es solo un ejemplo. Reemplazar su clave antes de ejecutarlo. Configurar la cadena de conexion de la aplicacion para que use ese usuario.

La aplicacion permite configurar la cadena de conexion mediante la variable `GRANDT_TEST_CONNECTION_STRING`. Si no esta definida, usa la configuracion local predeterminada de `Conexion.cs`. En ambos casos, la cadena debe apuntar a la base `GranDT` y usar un usuario con los permisos del script.

## Ejecutar la API

Desde la raiz:

```powershell
dotnet run --project GranDT_API/BibliotecaApi.csproj
```

En Development, Swagger queda disponible en `/swagger`.

## Operaciones de plantilla

- `GET /api/Plantilla/usuario/{email}` devuelve la plantilla del usuario con titulares, suplentes e historial de puntuaciones.
- `GET /api/Plantilla/{id}/validacion` devuelve las validaciones de presupuesto, cantidad y formacion titular.
- `POST /api/PlantillaJugador` incorpora un jugador a una plantilla; los triggers controlan presupuesto, limite y cupos por posicion.
- `POST /api/Puntuacion?idJugador={id}` registra una nota para una fecha del torneo.

Una plantilla se considera completa cuando tiene exactamente 1 arquero, 4 defensores, 3 mediocampistas y 2 delanteros titulares. Los suplentes no entran en el puntaje de fecha.

## Estructura

- `GranDT_Clases/`: modelos, reglas de negocio, servicios, interfaces y persistencia.
- `GranDT_API/`: API REST.
- `TestClases.tests/`: pruebas unitarias y de integracion.
- `doc/`: scripts SQL, diagramas y bitacora.

## Documentacion

- [Diagrama entidad-relacion](doc/DER.md)
- [Diagrama de clases](doc/Diagrama%20de%20Clases.md)
- [Bitacora](doc/Bitacora.md)
