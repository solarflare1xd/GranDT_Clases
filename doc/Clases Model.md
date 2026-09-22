```mermaid
classDiagram
     %% Entidades de Dominio
    class Usuario {
        +Nombre: string
        +Apellido: string
        +Email: string
        +Nacimiento: DateOnly
        +Password: string
        +EsAdministrador: bool
        +PlantillaUsuario: Plantilla
    }

    class Plantilla {
        +IdPlantilla: string
        +Presupuesto: float
        +Jugadores: List~PlantillaJugador~
    }

    class PlantillaJugador {
        +IdPlantilla: string
        +IdJugador: int
        +Numero: int
        +EsSuplente: bool
        +Plantilla: Plantilla
        +Futbolista: Futbolista
    }

    class Equipo {
        +Nombre: string
        +Jugadores: List~Futbolista~
    }

    class Jugador {
        +IdJugador: int
        +Nombre: string
        +Apellido: string
        +Apodo: string
        +Precio: float
        +FechaNacimiento: DateOnly
        +Posicion: string
        +HistorialPuntajes: List~Puntuacion~
    }

    class Puntuacion {
        +IdPuntuacion: string
        +Partido_date: DateOnly
        +Puntaje: int
        +Futbolista: Futbolista
    }

    Usuario "1" --> "1" Plantilla : PlantillaUsuario
    Plantilla "1" *-- "N" PlantillaJugador : Jugadores
    PlantillaJugador "N" --> "1" Plantilla : Plantilla
    PlantillaJugador "N" --> "1" Jugador : Futbolista
    Equipo "1" --> "N" Jugador : Jugadores
    Jugador "1" --> "N" Puntuacion : HistorialPuntajes
    Puntuacion "N" --> "1" Jugador : Futbolista 
    ```.