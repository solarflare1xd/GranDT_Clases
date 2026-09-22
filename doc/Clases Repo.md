```mermaid
classDiagram
    direction TB

    %% Interfaces
    class IUsuarioRepository {
        <<interface>>
        +ObtenerTodos() List~Usuario~
        +ObtenerPorEmail(email: string) Usuario?
        +Agregar(usuario: Usuario) Usuario
        +Eliminar(email: string) bool
    }

    class IPlantillaRepository {
        <<interface>>
        +ObtenerTodos() List~Plantilla~
        +ObtenerPorId(idPlantilla: string) Plantilla?
        +Agregar(plantilla: Plantilla) Plantilla
        +Eliminar(idPlantilla: string) bool
    }

    class IPlantillaJugadorRepository {
        <<interface>>
        +ObtenerTodos() List~PlantillaJugador~
        +ObtenerPorId(idPlantilla: string, idJugador: int) PlantillaJugador?
        +Agregar(plantillaJugador: PlantillaJugador) PlantillaJugador
        +Eliminar(idPlantilla: string, idJugador: int) bool
    }

    class IEquipoRepository {
        <<interface>>
        +ObtenerTodos() List~Equipo~
        +ObtenerPorNombre(nombre: string) Equipo?
        +Agregar(equipo: Equipo) Equipo
        +Eliminar(nombre: string) bool
    }

    class IJugadorRepository {
        <<interface>>
        +ObtenerTodos() List~Futbolista~
        +ObtenerPorId(id: int) Futbolista?
        +Agregar(jugador: Futbolista) Futbolista
        +Eliminar(id: int) bool
    }

    class IPuntuacionRepository {
        <<interface>>
        +ObtenerTodos() List~Puntuacion~
        +ObtenerPorId(idPuntuacion: string) Puntuacion?
        +Agregar(puntuacion: Puntuacion) Puntuacion
        +Eliminar(idPuntuacion: string) bool
    }

    %% Clases Repositorio en Memoria
    class EquipoRepositoryMemoria {
        -equipos: List~Equipo~
        +ObtenerTodos() List~Equipo~
        +ObtenerPorNombre(nombre: string) Equipo?
        +Agregar(equipo: Equipo) Equipo
        +Eliminar(nombre: string) bool
    }

    class JugadorRepositoryMemoria {
        -jugadores: List~Futbolista~
        +ObtenerTodos() List~Futbolista~
        +ObtenerPorId(id: int) Futbolista?
        +Agregar(jugador: Futbolista) Futbolista
        +Eliminar(id: int) bool
    }

    class PlantillaJugadorRepositoryMemoria {
        -plantillaJugadores: List~PlantillaJugador~
        +ObtenerTodos() List~PlantillaJugador~
        +ObtenerPorId(idPlantilla: string, idJugador: int) PlantillaJugador?
        +Agregar(plantillaJugador: PlantillaJugador) PlantillaJugador
        +Eliminar(idPlantilla: string, idJugador: int) bool
    }

    class PlantillaRepositoryMemoria {
        -Plantillas: List~Plantilla~
        +ObtenerTodos() List~Plantilla~
        +ObtenerPorId(id: string) Plantilla?
        +Agregar(plantilla: Plantilla) Plantilla
        +Eliminar(id: string) bool
    }

    class PuntuacionRepositoryMemoria {
        -Puntuaciones: List~Puntuacion~
        +ObtenerTodos() List~Puntuacion~
        +ObtenerPorId(id: string) Puntuacion?
        +Agregar(puntuacion: Puntuacion) Puntuacion
        +Eliminar(id: string) bool
    }

    class UsuarioRepositoryMemoria {
        -usuarios: List~Usuario~
        +ObtenerTodos() List~Usuario~
        +ObtenerPorEmail(email: string) Usuario?
        +Agregar(usuario: Usuario) Usuario
        +Eliminar(email: string) bool
    }

   
    %% Relaciones de Implementación de Repositorios
    IEquipoRepository <|.. EquipoRepositoryMemoria : implementa
    IJugadorRepository <|.. JugadorRepositoryMemoria : implementa
    IPlantillaJugadorRepository <|.. PlantillaJugadorRepositoryMemoria : implementa
    IPlantillaRepository <|.. PlantillaRepositoryMemoria : implementa
    IPuntuacionRepository <|.. PuntuacionRepositoryMemoria : implementa
    IUsuarioRepository <|.. UsuarioRepositoryMemoria : implementa

    %% Relaciones del Dominio


    ```.