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

## Crear plantilla

`POST /api/Plantilla` recibe `{ "idPlantilla": "PLANT-001" }`. El presupuesto se toma de la configuracion global de la base.

## Agregar futbolista a una plantilla

`POST /api/PlantillaJugador`

```json
{
  "idPlantilla": "PLANT-001",
  "idJugador": 12,
  "numero": 1,
  "esSuplente": false
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
