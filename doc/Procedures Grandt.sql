DELIMITER //

-- =========================================
-- PROCEDURES EQUIPO
-- =========================================

-- Obtener todos los equipos junto con sus jugadores y la posición de cada uno
CREATE PROCEDURE sp_ObtenerTodosEquipos()
BEGIN
    SELECT 
        e.Nombre,
        j.IdJugador, j.Nombre AS JugadorNombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento, 
        pos.IdPosicion, pos.Nombre
    FROM Equipo e
    LEFT JOIN Jugador j ON e.IdEquipo = j.IdEquipo
    LEFT JOIN Posicion pos ON j.IdPosicion = pos.IdPosicion;
END //

-- Obtener un equipo por su nombre junto con sus jugadores
CREATE PROCEDURE sp_ObtenerEquipoPorNombre(
    IN p_Nombre VARCHAR(100)
)
BEGIN
    SELECT 
        e.Nombre,
        j.IdJugador, j.Nombre AS JugadorNombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento, 
        pos.IdPosicion, pos.Nombre
    FROM Equipo e
    LEFT JOIN Jugador j ON e.IdEquipo = j.IdEquipo
    LEFT JOIN Posicion pos ON j.IdPosicion = pos.IdPosicion
    WHERE e.Nombre = p_Nombre;
END //

-- Agregar un equipo
CREATE PROCEDURE sp_AgregarEquipo(
    IN p_Nombre VARCHAR(100)
)
BEGIN
    INSERT INTO Equipo (Nombre) VALUES (p_Nombre);
END //

-- Eliminar un equipo por nombre
CREATE PROCEDURE sp_EliminarEquipo(
    IN p_Nombre VARCHAR(100)
)
BEGIN
    DECLARE v_IdEquipo INT;
    
    SELECT IdEquipo INTO v_IdEquipo FROM Equipo WHERE Nombre = p_Nombre LIMIT 1;
    
    IF v_IdEquipo IS NOT NULL THEN
        -- Desvincular jugadores antes de eliminar el equipo
        UPDATE Jugador SET IdEquipo = NULL WHERE IdEquipo = v_IdEquipo;
        DELETE FROM Equipo WHERE IdEquipo = v_IdEquipo;
    END IF;
END //

-- =========================================
-- PROCEDURES JUGADOR
-- =========================================

-- Obtener todos los jugadores con su posición y su historial de puntuaciones
CREATE PROCEDURE sp_ObtenerTodosJugadores()
BEGIN
    SELECT 
        j.IdJugador, j.Nombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento, 
        pos.IdPosicion, pos.Nombre, 
        p.IdPuntuacion, p.Partido_date, p.Puntaje, p.IdJugador
    FROM Jugador j
    LEFT JOIN Posicion pos ON j.IdPosicion = pos.IdPosicion
    LEFT JOIN Puntuacion p ON j.IdJugador = p.IdJugador;
END //

-- Obtener un jugador por su ID con su posición y su historial de puntuaciones
CREATE PROCEDURE sp_ObtenerJugadorPorId(
    IN p_IdJugador INT
)
BEGIN
    SELECT 
        j.IdJugador, j.Nombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento, 
        pos.IdPosicion, pos.Nombre, 
        p.IdPuntuacion, p.Partido_date, p.Puntaje, p.IdJugador
    FROM Jugador j
    LEFT JOIN Posicion pos ON j.IdPosicion = pos.IdPosicion
    LEFT JOIN Puntuacion p ON j.IdJugador = p.IdJugador
    WHERE j.IdJugador = p_IdJugador;
END //

-- Agregar Jugador ahora pide p_IdPosicion (INT)
CREATE PROCEDURE sp_AgregarJugador(
    IN p_Nombre VARCHAR(50),
    IN p_Apellido VARCHAR(50),
    IN p_Apodo VARCHAR(50),
    IN p_Precio FLOAT,
    IN p_FechaNacimiento DATE,
    IN p_IdPosicion INT
)
BEGIN
    -- Se inserta el IdPosicion en lugar del string. IdEquipo queda en NULL temporalmente
    INSERT INTO Jugador (Nombre, Apellido, Apodo, Precio, FechaNacimiento, IdPosicion, IdEquipo) 
    VALUES (p_Nombre, p_Apellido, p_Apodo, p_Precio, p_FechaNacimiento, p_IdPosicion, NULL);
    
    SELECT LAST_INSERT_ID();
END //

CREATE PROCEDURE sp_EliminarJugador(
    IN p_IdJugador INT
)
BEGIN
    DELETE FROM Jugador WHERE IdJugador = p_IdJugador;
END //

-- =========================================
-- PROCEDURES PLANTILLA
-- =========================================

-- Obtener todas las plantillas
CREATE PROCEDURE sp_ObtenerTodasPlantillas()
BEGIN
    SELECT IdPlantilla, Presupuesto 
    FROM Plantilla;
END //

-- Obtener plantilla por Id
CREATE PROCEDURE sp_ObtenerPlantillaPorId(
    IN p_IdPlantilla VARCHAR(50)
)
BEGIN
    SELECT IdPlantilla, Presupuesto 
    FROM Plantilla 
    WHERE IdPlantilla = p_IdPlantilla;
END //

-- Agregar una plantilla
CREATE PROCEDURE sp_AgregarPlantilla(
    IN p_IdPlantilla VARCHAR(50),
    IN p_Presupuesto FLOAT
)
BEGIN
    INSERT INTO Plantilla (IdPlantilla, Presupuesto) 
    VALUES (p_IdPlantilla, p_Presupuesto);
END //

-- Eliminar una plantilla
CREATE PROCEDURE sp_EliminarPlantilla(
    IN p_IdPlantilla VARCHAR(50)
)
BEGIN
    DELETE FROM Plantilla WHERE IdPlantilla = p_IdPlantilla;
END //

-- =========================================
-- PROCEDURES PLANTILLA-JUGADOR
-- =========================================

-- Obtener todos los registros de PlantillaJugador
CREATE PROCEDURE sp_ObtenerTodasPlantillaJugadores()
BEGIN
    SELECT IdPlantilla, IdJugador, Numero, EsSuplente 
    FROM PlantillaJugador;
END //

-- Obtener un registro específico por su clave compuesta
CREATE PROCEDURE sp_ObtenerPlantillaJugadorPorId(
    IN p_IdPlantilla VARCHAR(50),
    IN p_IdJugador INT
)
BEGIN
    SELECT IdPlantilla, IdJugador, Numero, EsSuplente 
    FROM PlantillaJugador 
    WHERE IdPlantilla = p_IdPlantilla AND IdJugador = p_IdJugador;
END //

-- Agregar un jugador a una plantilla
CREATE PROCEDURE sp_AgregarPlantillaJugador(
    IN p_IdPlantilla VARCHAR(50),
    IN p_IdJugador INT,
    IN p_Numero INT,
    IN p_EsSuplente BIT
)
BEGIN
    INSERT INTO PlantillaJugador (IdPlantilla, IdJugador, Numero, EsSuplente) 
    VALUES (p_IdPlantilla, p_IdJugador, p_Numero, p_EsSuplente);
END //

-- Eliminar un jugador de una plantilla
CREATE PROCEDURE sp_EliminarPlantillaJugador(
    IN p_IdPlantilla VARCHAR(50),
    IN p_IdJugador INT
)
BEGIN
    DELETE FROM PlantillaJugador 
    WHERE IdPlantilla = p_IdPlantilla AND IdJugador = p_IdJugador;
END //

-- =========================================
-- PROCEDURES POSICION
-- =========================================

-- Obtener todas las posiciones
CREATE PROCEDURE sp_ObtenerTodasPosiciones()
BEGIN
    SELECT IdPosicion, Nombre 
    FROM Posicion;
END //

-- Obtener una posición por su Id
CREATE PROCEDURE sp_ObtenerPosicionPorId(
    IN p_IdPosicion INT
)
BEGIN
    SELECT IdPosicion, Nombre 
    FROM Posicion 
    WHERE IdPosicion = p_IdPosicion;
END //

-- Agregar una nueva posición
CREATE PROCEDURE sp_AgregarPosicion(
    IN p_Nombre VARCHAR(50)
)
BEGIN
    INSERT INTO Posicion (Nombre) VALUES (p_Nombre);
    -- Devolvemos el ID generado automáticamente
    SELECT LAST_INSERT_ID();
END //

-- Eliminar una posición
CREATE PROCEDURE sp_EliminarPosicion(
    IN p_IdPosicion INT
)
BEGIN
    DELETE FROM Posicion WHERE IdPosicion = p_IdPosicion;
END //

-- =========================================
-- PROCEDURES USUARIO
-- =========================================

-- Obtener todos los usuarios
CREATE PROCEDURE sp_ObtenerTodosUsuarios()
BEGIN
    SELECT Email, Nombre, Apellido, Nacimiento, Password, EsAdministrador, IdPlantilla 
    FROM Usuario;
END //

-- Obtener usuario por Email
CREATE PROCEDURE sp_ObtenerUsuarioPorEmail(
    IN p_Email VARCHAR(100)
)
BEGIN
    SELECT Email, Nombre, Apellido, Nacimiento, Password, EsAdministrador, IdPlantilla 
    FROM Usuario 
    WHERE Email = p_Email;
END //

-- Agregar un usuario
CREATE PROCEDURE sp_AgregarUsuario(
    IN p_Email VARCHAR(100),
    IN p_Nombre VARCHAR(50),
    IN p_Apellido VARCHAR(50),
    IN p_Nacimiento DATE,
    IN p_Password VARCHAR(255),
    IN p_EsAdministrador BIT,
    IN p_IdPlantilla VARCHAR(50)
)
BEGIN
    INSERT INTO Usuario (Email, Nombre, Apellido, Nacimiento, Password, EsAdministrador, IdPlantilla) 
    VALUES (p_Email, p_Nombre, p_Apellido, p_Nacimiento, p_Password, p_EsAdministrador, p_IdPlantilla);
END //

-- Eliminar un usuario
CREATE PROCEDURE sp_EliminarUsuario(
    IN p_Email VARCHAR(100)
)
BEGIN
    DELETE FROM Usuario WHERE Email = p_Email;
END //

DELIMITER ;