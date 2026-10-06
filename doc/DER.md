# Diagrama entidad-relacion

```mermaid
erDiagram
    EQUIPO ||--o{ JUGADOR : tiene
    POSICION ||--o{ JUGADOR : clasifica
    PLANTILLA ||--o| USUARIO : pertenece
    PLANTILLA ||--o{ PLANTILLA_JUGADOR : contiene
    JUGADOR ||--o{ PLANTILLA_JUGADOR : integra
    JUGADOR ||--o{ PUNTUACION : recibe

    EQUIPO {
        int IdEquipo PK
        varchar Nombre UK
    }
    POSICION {
        int IdPosicion PK
        varchar Nombre UK
    }
    JUGADOR {
        int IdJugador PK
        varchar Nombre
        varchar Apellido
        varchar Apodo
        decimal Precio
        date FechaNacimiento
        int IdPosicion FK
        int IdEquipo FK
    }
    PLANTILLA {
        varchar IdPlantilla PK
        decimal Presupuesto
    }
    USUARIO {
        varchar Email PK
        varchar Nombre
        varchar Apellido
        date Nacimiento
        char Password
        bit EsAdministrador
        varchar IdPlantilla FK
    }
    PLANTILLA_JUGADOR {
        varchar IdPlantilla PK, FK
        int IdJugador PK, FK
        int Numero
        bit EsSuplente
    }
    PUNTUACION {
        varchar IdPuntuacion PK
        tinyint Fecha
        decimal Puntaje
        int IdJugador FK
    }
```

Cada usuario puede tener como maximo una plantilla y cada plantilla como maximo un usuario. La tabla `PlantillaJugador` representa la relacion muchos-a-muchos entre plantillas y jugadores. Al crear una plantilla, el usuario elige su presupuesto, que debe ser mayor que 0 y no superar $99.999.999,99. El maximo actual es de 20 jugadores.
