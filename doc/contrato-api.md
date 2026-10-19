# Contrato de la API Gran DT

Este documento describe los endpoints indicados para la API. Los ejemplos muestran JSON con nombres de propiedades en `camelCase`; las fechas se representan en formato ISO 8601 (`YYYY-MM-DD`). Los identificadores y valores son ilustrativos.

## Códigos de respuesta y errores

- `200 OK`: consulta o actualización completada.
- `201 Created`: recurso creado.
- `400 Bad Request`: datos de entrada inválidos o incompatibles con las reglas de negocio.
- `404 Not Found`: no existe el recurso solicitado o una referencia necesaria.
- `409 Conflict`: el recurso ya existe o la operación genera un duplicado, cuando corresponda.

En las operaciones de escritura, los datos inválidos devuelven `400`; los recursos inexistentes, `404`; y los duplicados, `409` cuando corresponda.

Las cuentas `administrador` e `invitado` mencionadas para este proyecto son cuentas de MySQL. No son roles de autenticación HTTP y este contrato no requiere encabezados o credenciales de autorización asociados a esos nombres.

En las rutas `/api/usuarios/{id}/plantillas`, `{id}` representa el email del usuario, codificado como segmento de URL. El esquema vigente usa el email como clave primaria y no define un identificador numérico de usuario.

## Equipos

### `GET /api/equipos`

Devuelve la lista de equipos.

**Entradas:** ninguna.

**Respuesta `200 OK`:**

```json
[
  {
    "nombre": "Boca Juniors",
    "jugadores": []
  },
  {
    "nombre": "River Plate",
    "jugadores": []
  }
]
```

## Futbolistas

### `GET /api/futbolistas`

Devuelve la lista de futbolistas con su equipo y cotización.

**Entradas:** ninguna.

**Respuesta `200 OK`:**

```json
[
  {
    "idJugador": 12,
    "nombre": "Marcos",
    "apellido": "Rojo",
    "apodo": null,
    "precio": 1250000.00,
    "fechaNacimiento": "1990-03-20",
    "idEquipo": 1,
    "equipo": {
      "nombre": "Boca Juniors"
    },
    "posicion": {
      "idPosicion": 2,
      "nombre": "Defensor"
    },
    "historialPuntajes": []
  }
]
```

### `POST /api/futbolistas`

Crea un futbolista.

**Entradas — cuerpo JSON:**

| Campo | Tipo | Descripción |
| --- | --- | --- |
| `nombre` | string | Nombre del futbolista. |
| `apellido` | string | Apellido del futbolista. |
| `apodo` | string o `null` | Apodo opcional. |
| `precio` | decimal | Cotización del futbolista. |
| `fechaNacimiento` | string (`YYYY-MM-DD`) | Fecha de nacimiento. |
| `idPosicion` | integer | Identificador de la posición. |
| `idEquipo` | integer o `null` | Identificador del equipo, si corresponde. |

```json
{
  "nombre": "Marcos",
  "apellido": "Rojo",
  "apodo": null,
  "precio": 1250000.00,
  "fechaNacimiento": "1990-03-20",
  "idPosicion": 2,
  "idEquipo": 1
}
```

**Respuesta `201 Created`:**

```json
{
  "idJugador": 12,
  "nombre": "Marcos",
  "apellido": "Rojo",
  "apodo": null,
  "precio": 1250000.00,
  "fechaNacimiento": "1990-03-20",
  "idEquipo": 1,
  "equipo": {
    "nombre": "Boca Juniors"
  },
  "posicion": {
    "idPosicion": 2,
    "nombre": "Defensor"
  },
  "historialPuntajes": []
}
```

**Errores:** `400` si los datos son inválidos; `404` si una posición o equipo referenciado no existe; `409` si corresponde por duplicidad.

### `PUT /api/futbolistas/{id}`

Actualiza equipo y cotización de un futbolista.

**Entradas:**

- Parámetro de ruta `id` (integer): identificador del futbolista.
- Cuerpo JSON:

```json
{
  "idEquipo": 2,
  "precio": 1300000.00
}
```

**Respuesta `200 OK`:**

```json
{
  "idJugador": 12,
  "nombre": "Marcos",
  "apellido": "Rojo",
  "apodo": null,
  "precio": 1300000.00,
  "fechaNacimiento": "1990-03-20",
  "idEquipo": 2,
  "equipo": {
    "nombre": "River Plate"
  },
  "posicion": {
    "idPosicion": 2,
    "nombre": "Defensor"
  },
  "historialPuntajes": []
}
```

**Errores:** `400` si los datos son inválidos; `404` si no existe el futbolista o el equipo indicado.

### `POST /api/futbolistas/{id}/puntuaciones`

Registra una puntuación para el futbolista indicado.

**Entradas:**

- Parámetro de ruta `id` (integer): identificador del futbolista.
- Cuerpo JSON:

| Campo | Tipo | Descripción |
| --- | --- | --- |
| `idPuntuacion` | string | Identificador de la puntuación. |
| `fecha` | integer | Número de fecha del torneo. |
| `puntaje` | decimal | Puntaje obtenido. |

```json
{
  "idPuntuacion": "PUNT-2026-001",
  "fecha": 1,
  "puntaje": 8.5
}
```

**Respuesta `201 Created`:**

```json
{
  "idPuntuacion": "PUNT-2026-001",
  "fecha": 1,
  "puntaje": 8.5,
  "idJugador": 12
}
```

**Errores:** `400` si los datos son inválidos; `404` si no existe el futbolista; `409` si la puntuación ya fue registrada.

## Usuarios

### `POST /api/usuarios`

Crea un usuario. La contraseña se envía en la solicitud, pero nunca se incluye en la respuesta.

**Entradas — cuerpo JSON:**

| Campo | Tipo | Descripción |
| --- | --- | --- |
| `nombre` | string | Nombre del usuario. |
| `apellido` | string | Apellido del usuario. |
| `email` | string | Email del usuario. |
| `nacimiento` | string (`YYYY-MM-DD`) | Fecha de nacimiento. |
| `password` | string | Contraseña del usuario. |

```json
{
  "nombre": "Ana",
  "apellido": "Pérez",
  "email": "ana@example.com",
  "nacimiento": "1995-06-12",
  "password": "una-clave-segura"
}
```

**Respuesta `201 Created`:**

```json
{
  "nombre": "Ana",
  "apellido": "Pérez",
  "email": "ana@example.com",
  "nacimiento": "1995-06-12",
  "esAdministrador": false
}
```

**Errores:** `400` si los datos son inválidos; `409` si ya existe un usuario con ese email.

## Plantillas

### `GET /api/usuarios/{id}/plantillas`

Devuelve las plantillas asociadas al usuario.

**Entradas:** parámetro de ruta `id` (integer), identificador del usuario.

**Respuesta `200 OK`:**

```json
[
  {
    "idPlantilla": 31,
    "presupuesto": 50000000.00,
    "cantidadMaximaJugadores": 20,
    "jugadores": [],
    "jugadores_sup": []
  }
]
```

**Error:** `404` si no existe el usuario.

### `POST /api/usuarios/{id}/plantillas`

Crea una plantilla para el usuario indicado.

**Entradas:**

- Parámetro de ruta `id` (integer): identificador del usuario.
- Cuerpo JSON:

| Campo | Tipo | Descripción |
| --- | --- | --- |
| `presupuesto` | decimal | Presupuesto disponible para la plantilla. |
| `titulares` | array de integer | Identificadores de futbolistas titulares. |
| `suplentes` | array de integer | Identificadores de futbolistas suplentes. |

```json
{
  "presupuesto": 50000000.00,
  "titulares": [12, 15],
  "suplentes": [18]
}
```

**Respuesta `201 Created`:**

```json
{
  "idPlantilla": 31,
  "presupuesto": 50000000.00,
  "cantidadMaximaJugadores": 20,
  "jugadores": [
    {
      "idJugador": 12,
      "nombre": "Marcos",
      "apellido": "Rojo",
      "precio": 1250000.00
    },
    {
      "idJugador": 15,
      "nombre": "Jugador",
      "apellido": "Titular",
      "precio": 1100000.00
    }
  ],
  "jugadores_sup": [
    {
      "idJugador": 18,
      "nombre": "Jugador",
      "apellido": "Suplente",
      "precio": 900000.00
    }
  ]
}
```

**Errores:** `400` si el presupuesto, la formación o los integrantes no son válidos; `404` si no existe el usuario o alguno de los futbolistas indicados.

### `POST /api/plantillas/{id}/jugadores`

Agrega un futbolista a una plantilla.

**Entradas:**

- Parámetro de ruta `id` (integer): identificador de la plantilla.
- Cuerpo JSON:

| Campo | Tipo | Descripción |
| --- | --- | --- |
| `idJugador` | integer | Identificador del futbolista que se agregará. |
| `numero` | integer | Número asignado al integrante de la plantilla. |
| `esSuplente` | boolean | `true` si se agrega como suplente; `false` si es titular. |

```json
{
  "idJugador": 18,
  "numero": 12,
  "esSuplente": true
}
```

**Respuesta `200 OK`:** plantilla actualizada.

```json
{
  "idPlantilla": 31,
  "presupuesto": 50000000.00,
  "cantidadMaximaJugadores": 20,
  "jugadores": [],
  "jugadores_sup": [
    {
      "idJugador": 18,
      "nombre": "Jugador",
      "apellido": "Suplente",
      "precio": 900000.00
    }
  ]
}
```

**Errores:** `400` si los datos o las reglas de la plantilla no se cumplen; `404` si no existe la plantilla o el futbolista; `409` si el futbolista ya pertenece a la plantilla o existe otro conflicto de duplicidad.

### `PUT /api/plantillas/{id}/jugadores/{futbolistaId}`

Actualiza la condición de titular o suplente de un futbolista que pertenece a la plantilla.

**Entradas:**

- Parámetro de ruta `id` (integer): identificador de la plantilla.
- Parámetro de ruta `futbolistaId` (integer): identificador del futbolista.
- Cuerpo JSON:

```json
{
  "esSuplente": false
}
```

**Respuesta `200 OK`:**

```json
{
  "idPlantilla": 31,
  "idJugador": 18,
  "numero": 12,
  "esSuplente": false
}
```

**Errores:** `400` si el valor de `esSuplente` es inválido o no se puede aplicar según las reglas de negocio; `404` si no existe la plantilla o el futbolista no pertenece a ella.
