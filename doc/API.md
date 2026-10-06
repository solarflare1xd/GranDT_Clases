# Cuerpos de las solicitudes de la API

Los `POST` reciben DTOs de entrada: se envian solamente los valores necesarios para la operacion, no objetos relacionados completos ni propiedades calculadas.

Las fechas deben enviarse en formato ISO `yyyy-MM-dd` (por ejemplo, el 5 de diciembre de 2000 se envia como `"2000-12-05"`), no como `"05/12/2000"`.

## Crear usuario

`POST /api/Usuario`

```json
{
  "nombre": "Ana",
  "apellido": "Perez",
  "email": "ana@example.com",
  "nacimiento": "2000-04-12",
  "password": "una-clave",
  "esAdministrador": false,
  "idPlantilla": null
}
```

`idPlantilla` es opcional y, si se envía, debe ser el ID numérico de una plantilla existente. Las respuestas de usuario no incluyen la contrasena almacenada.

## Crear futbolista

`GET /api/Futbolista?nombre=Juan` busca futbolistas cuyo nombre contenga el texto indicado. La búsqueda no distingue mayúsculas de minúsculas y devuelve una lista, que puede estar vacía si no hay coincidencias. Sin el parámetro `nombre`, `GET /api/Futbolista` sigue devolviendo todos los futbolistas.

`POST /api/Futbolista`

```json
{
  "nombre": "Ana",
  "apellido": "Perez",
  "apodo": null,
  "precio": 1250000.50,
  "fechaNacimiento": "2000-04-12",
  "idPosicion": 2,
  "idEquipo": 1
}
```

La API recibe los identificadores de posicion y equipo, no los objetos completos.

## Crear equipo y posicion

`POST /api/Equipo` recibe `{ "nombre": "Equipo Norte" }`.

`POST /api/Posicion` recibe `{ "nombre": "Defensor" }`.

## Crear plantilla

`POST /api/Plantilla` recibe el presupuesto y dos listas opcionales de IDs: `titulares` y `suplentes`. Ambas pueden omitirse, enviarse como `null` o estar vacías para crear una plantilla sin jugadores. El API asigna automáticamente el número de cada jugador según el orden de las listas. El ID de la plantilla se genera automáticamente y se devuelve en la respuesta. El presupuesto debe ser mayor que 0; el límite técnico es $9.999.999.999.999.999,99 por el tipo `DECIMAL(18, 2)` de la base. El máximo de jugadores es 20. Los jugadores deben existir. Al crear o completar la plantilla se rechazan IDs repetidos, el exceso de presupuesto o jugadores, y más de 1 arquero, 4 defensores, 3 mediocampistas o 3 delanteros titulares.

Para crearla vacía y agregar jugadores después:

```json
{
  "presupuesto": 100000000,
  "titulares": null,
  "suplentes": null
}
```

```json
{
  "presupuesto": 31000000.00,
  "titulares": [1, 3, 4, 5, 6, 7, 8, 9, 11, 12, 21],
  "suplentes": [2]
}
```

Con los datos de `04 Inserts.sql`, esta selección cuesta $30.600.000,50. Los IDs dependen de los datos cargados en tu base; consultá `GET /api/Futbolista` para verificar los IDs, posiciones y precios disponibles antes de enviar la solicitud.

La plantilla y los integrantes enviados se guardan en una transacción: si alguna operación falla, no queda una plantilla parcial en la base. Se puede crear vacía y agregar jugadores después:

`POST /api/PlantillaJugador`

```json
{
  "idPlantilla": 1,
  "idJugador": 5,
  "numero": 1,
  "esSuplente": false
}
```

Repetí la solicitud para cada jugador. Al completar los 11 titulares, la formación debe ser de 1 arquero, 4 defensores, 3 mediocampistas y 3 delanteros.

Para ampliar una base existente sin borrar sus datos, ejecutá `06 Migrar presupuesto.sql`, luego volvé a ejecutar `02 Triggers.sql` y `03 Procedures Grandt.sql` para actualizar las validaciones y el procedimiento de creación.

## Intercambiar titular y suplente

`PATCH /api/PlantillaJugador/{idPlantilla}/intercambiar-titulares` intercambia un titular por un suplente de la misma posicion. `idPlantilla` es el entero generado al crear la plantilla. La base verifica que la plantilla siga completa; el cambio se revierte si no pasa esa validacion.

```json
{
  "idJugadorTitular": 4,
  "idJugadorSuplente": 12
}
```

## Registrar puntuacion

`POST /api/Puntuacion`

```json
{
  "idPuntuacion": "PUNT-001",
  "fecha": 1,
  "puntaje": 8.5,
  "idJugador": 12
}
```
