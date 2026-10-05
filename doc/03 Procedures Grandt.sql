DELIMITER //

-- =========================================
-- PROCEDURES EQUIPO
-- =========================================

-- Obtener todos los equipos junto con sus jugadores y la posición de cada uno
CREATE PROCEDURE sp_ObtenerTodosEquipos()
BEGIN
    SELECT 
        e.Nombre,
        j.IdJugador, j.Nombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento, j.IdEquipo,
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
        j.IdJugador, j.Nombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento, j.IdEquipo,
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
        j.IdJugador, j.Nombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento, j.IdEquipo,
        pos.IdPosicion, pos.Nombre, 
        p.IdPuntuacion, p.Fecha, p.Puntaje, p.IdJugador
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
        j.IdJugador, j.Nombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento, j.IdEquipo,
        pos.IdPosicion, pos.Nombre, 
        p.IdPuntuacion, p.Fecha, p.Puntaje, p.IdJugador
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
    IN p_Precio DECIMAL(10, 2),
    IN p_FechaNacimiento DATE,
    IN p_IdPosicion INT
)
BEGIN
    -- Se inserta el IdPosicion en lugar del string. IdEquipo queda en NULL temporalmente
    INSERT INTO Jugador (Nombre, Apellido, Apodo, Precio, FechaNacimiento, IdPosicion, IdEquipo) 
    VALUES (p_Nombre, p_Apellido, p_Apodo, p_Precio, p_FechaNacimiento, p_IdPosicion, NULL);
    
    SELECT LAST_INSERT_ID();
END //

CREATE PROCEDURE sp_AgregarJugadorConEquipo(
    IN p_Nombre VARCHAR(50),
    IN p_Apellido VARCHAR(50),
    IN p_Apodo VARCHAR(50),
    IN p_Precio DECIMAL(10, 2),
    IN p_FechaNacimiento DATE,
    IN p_IdPosicion INT,
    IN p_IdEquipo INT
)
BEGIN
    INSERT INTO Jugador (Nombre, Apellido, Apodo, Precio, FechaNacimiento, IdPosicion, IdEquipo)
    VALUES (p_Nombre, p_Apellido, p_Apodo, p_Precio, p_FechaNacimiento, p_IdPosicion, p_IdEquipo);

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
    SELECT p.IdPlantilla, c.PresupuestoMaximo AS Presupuesto, c.CantidadMaximaJugadores
    FROM Plantilla p
    INNER JOIN Configuracion c ON c.IdConfiguracion = 1;
END //

-- Obtener plantilla por Id
CREATE PROCEDURE sp_ObtenerPlantillaPorId(
    IN p_IdPlantilla VARCHAR(50)
)
BEGIN
    SELECT p.IdPlantilla, c.PresupuestoMaximo AS Presupuesto, c.CantidadMaximaJugadores
    FROM Plantilla p
    INNER JOIN Configuracion c ON c.IdConfiguracion = 1
    WHERE p.IdPlantilla = p_IdPlantilla;
END //

-- Agregar una plantilla
CREATE PROCEDURE sp_AgregarPlantilla(
    IN p_IdPlantilla VARCHAR(50),
    IN p_Presupuesto DECIMAL(10, 2)
)
BEGIN
    INSERT INTO Plantilla (IdPlantilla, Presupuesto) 
    SELECT p_IdPlantilla, PresupuestoMaximo
    FROM Configuracion
    WHERE IdConfiguracion = 1;

    SELECT Presupuesto FROM Plantilla WHERE IdPlantilla = p_IdPlantilla;
END //

CREATE PROCEDURE sp_ActualizarConfiguracion(
    IN p_PresupuestoMaximo DECIMAL(10, 2),
    IN p_CantidadMaximaJugadores TINYINT UNSIGNED
)
BEGIN
    IF p_PresupuestoMaximo < 0 OR p_CantidadMaximaJugadores < 11 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La configuracion requiere presupuesto no negativo y al menos 11 jugadores.';
    END IF;

    IF EXISTS (
        SELECT 1
        FROM PlantillaJugador pj
        INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
        GROUP BY pj.IdPlantilla
        HAVING SUM(j.Precio) > p_PresupuestoMaximo
            OR COUNT(*) > p_CantidadMaximaJugadores
    ) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La nueva configuracion no es compatible con las plantillas existentes.';
    END IF;

    UPDATE Configuracion
    SET PresupuestoMaximo = p_PresupuestoMaximo,
        CantidadMaximaJugadores = p_CantidadMaximaJugadores
    WHERE IdConfiguracion = 1;

    UPDATE Plantilla
    SET Presupuesto = p_PresupuestoMaximo;
END //

CREATE PROCEDURE sp_ValidarPlantilla(
    IN p_IdPlantilla VARCHAR(50)
)
BEGIN
    DECLARE v_presupuesto DECIMAL(10, 2);
    DECLARE v_max_jugadores INT;

    SELECT PresupuestoMaximo, CantidadMaximaJugadores
    INTO v_presupuesto, v_max_jugadores
    FROM Configuracion
    WHERE IdConfiguracion = 1;

    SELECT
        p.IdPlantilla,
        COALESCE(SUM(j.Precio), 0) <= v_presupuesto AS PresupuestoValido,
        COUNT(pj.IdJugador) <= v_max_jugadores AS CantidadValida,
        COALESCE(SUM(CASE WHEN pj.EsSuplente = 0 AND pos.Nombre = 'Arquero' THEN 1 ELSE 0 END), 0) = 1
            AND COALESCE(SUM(CASE WHEN pj.EsSuplente = 0 AND pos.Nombre = 'Defensor' THEN 1 ELSE 0 END), 0) = 4
            AND COALESCE(SUM(CASE WHEN pj.EsSuplente = 0 AND pos.Nombre = 'Mediocampista' THEN 1 ELSE 0 END), 0) = 3
            AND COALESCE(SUM(CASE WHEN pj.EsSuplente = 0 AND pos.Nombre = 'Delantero' THEN 1 ELSE 0 END), 0) = 2 AS FormacionValida,
        COALESCE(SUM(j.Precio), 0) AS Gasto,
        v_presupuesto - COALESCE(SUM(j.Precio), 0) AS PresupuestoDisponible,
        COUNT(pj.IdJugador) AS CantidadJugadores
    FROM Plantilla p
    LEFT JOIN PlantillaJugador pj ON pj.IdPlantilla = p.IdPlantilla
    LEFT JOIN Jugador j ON j.IdJugador = pj.IdJugador
    LEFT JOIN Posicion pos ON pos.IdPosicion = j.IdPosicion
    WHERE p.IdPlantilla = p_IdPlantilla
    GROUP BY p.IdPlantilla;
END //

CREATE PROCEDURE sp_ObtenerPlantillaPorUsuario(
    IN p_Email VARCHAR(100)
)
BEGIN
    SELECT p.IdPlantilla, c.PresupuestoMaximo AS Presupuesto, c.CantidadMaximaJugadores
    FROM Usuario u
    INNER JOIN Plantilla p ON p.IdPlantilla = u.IdPlantilla
    INNER JOIN Configuracion c ON c.IdConfiguracion = 1
    WHERE u.Email = p_Email;

    SELECT
        pj.IdPlantilla, pj.IdJugador, pj.Numero, pj.EsSuplente,
        j.Nombre, j.Apellido, j.Apodo, j.Precio, j.FechaNacimiento,
        pos.IdPosicion, pos.Nombre
    FROM Usuario u
    INNER JOIN PlantillaJugador pj ON pj.IdPlantilla = u.IdPlantilla
    INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
    LEFT JOIN Posicion pos ON pos.IdPosicion = j.IdPosicion
    WHERE u.Email = p_Email
    ORDER BY pj.EsSuplente, pos.IdPosicion, j.Apellido, j.Nombre;

    SELECT p.IdJugador, p.Fecha, p.Puntaje
    FROM Usuario u
    INNER JOIN PlantillaJugador pj ON pj.IdPlantilla = u.IdPlantilla
    INNER JOIN Puntuacion p ON p.IdJugador = pj.IdJugador
    WHERE u.Email = p_Email;
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
    IN p_Nombre VARCHAR(30)
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
    SELECT Email, Nombre, Apellido, Nacimiento, EsAdministrador, IdPlantilla 
    FROM Usuario;
END //

-- Obtener usuario por Email
CREATE PROCEDURE sp_ObtenerUsuarioPorEmail(
    IN p_Email VARCHAR(100)
)
BEGIN
    SELECT Email, Nombre, Apellido, Nacimiento, EsAdministrador, IdPlantilla 
    FROM Usuario 
    WHERE Email = p_Email;
END //

-- Agregar un usuario
CREATE PROCEDURE sp_AgregarUsuario(
    IN p_Email VARCHAR(100),
    IN p_Nombre VARCHAR(50),
    IN p_Apellido VARCHAR(50),
    IN p_Nacimiento DATE,
    IN p_Password CHAR(64),
    IN p_EsAdministrador BIT,
    IN p_IdPlantilla VARCHAR(50)
)
BEGIN
    INSERT INTO Usuario (Email, Nombre, Apellido, Nacimiento, Password, EsAdministrador, IdPlantilla) 
    VALUES (p_Email, p_Nombre, p_Apellido, p_Nacimiento, p_Password, p_EsAdministrador, p_IdPlantilla);
END //

-- =========================================
-- PROCEDURES PUNTUACION
-- =========================================

CREATE PROCEDURE sp_ObtenerTodasPuntuaciones()
BEGIN
    SELECT IdPuntuacion, Fecha, Puntaje, IdJugador
    FROM Puntuacion;
END //

CREATE PROCEDURE sp_ObtenerPuntuacionPorId(
    IN p_IdPuntuacion VARCHAR(50)
)
BEGIN
    SELECT IdPuntuacion, Fecha, Puntaje, IdJugador
    FROM Puntuacion
    WHERE IdPuntuacion = p_IdPuntuacion;
END //

CREATE PROCEDURE sp_AgregarPuntuacion(
    IN p_IdPuntuacion VARCHAR(50),
    IN p_Fecha TINYINT UNSIGNED,
    IN p_Puntaje DECIMAL(3, 1),
    IN p_IdJugador INT
)
BEGIN
    INSERT INTO Puntuacion (IdPuntuacion, Fecha, Puntaje, IdJugador)
    VALUES (p_IdPuntuacion, p_Fecha, p_Puntaje, p_IdJugador);
END //

CREATE PROCEDURE sp_EliminarPuntuacion(
    IN p_IdPuntuacion VARCHAR(50)
)
BEGIN
    DELETE FROM Puntuacion WHERE IdPuntuacion = p_IdPuntuacion;
END //

-- Eliminar un usuario
CREATE PROCEDURE sp_EliminarUsuario(
    IN p_Email VARCHAR(100)
)
BEGIN
    DELETE FROM Usuario WHERE Email = p_Email;
END //

DELIMITER ;