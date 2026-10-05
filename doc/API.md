# Cuerpos de las solicitudes de la API

Los `POST` reciben DTOs de entrada: se envian solamente los valores necesarios para la operacion, no objetos relacionados completos ni propiedades calculadas.

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

`idPlantilla` es opcional. Las respuestas de usuario no incluyen la contrasena almacenada.

## Crear futbolista

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

## Crear plantilla completa

`POST /api/Plantilla` recibe la plantilla y sus integrantes en una solicitud. Se aplica un presupuesto fijo de $99.999.999,99 y un maximo fijo de 20 jugadores. Los jugadores deben existir. El service rechaza la solicitud si hay IDs repetidos, se excede el presupuesto o el maximo, o los titulares no cumplen la formacion de 1 arquero, 4 defensores, 4 mediocampistas y 2 delanteros.

```json
{
  "idPlantilla": "PLANT-001",
  "jugadores": [
    { "idJugador": 1, "numero": 1, "esSuplente": false },
    { "idJugador": 2, "numero": 2, "esSuplente": false },
    { "idJugador": 3, "numero": 3, "esSuplente": false },
    { "idJugador": 4, "numero": 4, "esSuplente": false },
    { "idJugador": 5, "numero": 5, "esSuplente": false },
    { "idJugador": 6, "numero": 6, "esSuplente": false },
    { "idJugador": 7, "numero": 7, "esSuplente": false },
    { "idJugador": 8, "numero": 8, "esSuplente": false },
    { "idJugador": 9, "numero": 9, "esSuplente": false },
    { "idJugador": 10, "numero": 10, "esSuplente": false },
    { "idJugador": 11, "numero": 11, "esSuplente": false },
    { "idJugador": 12, "numero": 12, "esSuplente": true }
  ]
}
```

La plantilla y sus integrantes se guardan en una transaccion: si alguna operacion falla, no queda una plantilla parcial en la base.

## Intercambiar titular y suplente

`PATCH /api/PlantillaJugador/{idPlantilla}/intercambiar-titulares` intercambia un titular por un suplente de la misma posicion. La base verifica que la plantilla siga completa; el cambio se revierte si no pasa esa validacion.

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
