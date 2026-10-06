USE GranDT;

DELIMITER //

DROP TRIGGER IF EXISTS TR_Equipo_LimiteInsert//
CREATE TRIGGER TR_Equipo_LimiteInsert
BEFORE INSERT ON Equipo
FOR EACH ROW
BEGIN
    IF (SELECT COUNT(*) FROM Equipo) >= 32 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'No se pueden registrar mas de 32 equipos.';
    END IF;
END//

DROP TRIGGER IF EXISTS TR_Jugador_ValidarInsert//
CREATE TRIGGER TR_Jugador_ValidarInsert
BEFORE INSERT ON Jugador
FOR EACH ROW
BEGIN
    IF (SELECT COUNT(*) FROM Jugador) >= 1500 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'No se pueden registrar mas de 1500 jugadores.';
    END IF;

    IF NEW.Precio < 0 OR NEW.Precio > 99999999.99 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El precio debe estar entre 0 y 99999999.99.';
    END IF;
END//

DROP TRIGGER IF EXISTS TR_Jugador_ValidarUpdate//
CREATE TRIGGER TR_Jugador_ValidarUpdate
BEFORE UPDATE ON Jugador
FOR EACH ROW
BEGIN
    IF NEW.Precio < 0 OR NEW.Precio > 99999999.99 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El precio debe estar entre 0 y 99999999.99.';
    END IF;

    IF NEW.Precio <> OLD.Precio AND EXISTS (
        SELECT 1
        FROM PlantillaJugador pj
        INNER JOIN Plantilla pl ON pl.IdPlantilla = pj.IdPlantilla
        INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
        WHERE pj.IdJugador = OLD.IdJugador
        GROUP BY pj.IdPlantilla, pl.Presupuesto
        HAVING SUM(j.Precio) - OLD.Precio + NEW.Precio > pl.Presupuesto
    ) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El cambio de precio supera el presupuesto de una plantilla.';
    END IF;

    IF NEW.IdPosicion <> OLD.IdPosicion AND EXISTS (
        SELECT 1 FROM PlantillaJugador WHERE IdJugador = OLD.IdJugador
    ) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'No se puede cambiar la posicion de un jugador incluido en una plantilla.';
    END IF;
END//

DROP TRIGGER IF EXISTS TR_Usuario_ValidarInsert//
CREATE TRIGGER TR_Usuario_ValidarInsert
BEFORE INSERT ON Usuario
FOR EACH ROW
BEGIN
    IF (SELECT COUNT(*) FROM Usuario) >= 2000 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'No se pueden registrar mas de 2000 usuarios.';
    END IF;
END//

DROP TRIGGER IF EXISTS TR_Usuario_ValidarUpdate//

DROP TRIGGER IF EXISTS TR_Plantilla_ValidarInsert//
CREATE TRIGGER TR_Plantilla_ValidarInsert
BEFORE INSERT ON Plantilla
FOR EACH ROW
BEGIN
    IF NEW.Presupuesto <= 0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El presupuesto debe ser mayor que 0.';
    END IF;
END//

DROP TRIGGER IF EXISTS TR_Plantilla_ValidarUpdate//
CREATE TRIGGER TR_Plantilla_ValidarUpdate
BEFORE UPDATE ON Plantilla
FOR EACH ROW
BEGIN
    DECLARE v_gasto DECIMAL(18, 2);

    IF NEW.Presupuesto <= 0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El presupuesto debe ser mayor que 0.';
    END IF;

    SELECT COALESCE(SUM(j.Precio), 0)
    INTO v_gasto
    FROM PlantillaJugador pj
    INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
    WHERE pj.IdPlantilla = OLD.IdPlantilla;

    IF v_gasto > NEW.Presupuesto THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El presupuesto no alcanza para cubrir el costo de los jugadores de la plantilla.';
    END IF;
END//

DROP TRIGGER IF EXISTS TR_PlantillaJugador_ValidarInsert//
CREATE TRIGGER TR_PlantillaJugador_ValidarInsert
BEFORE INSERT ON PlantillaJugador
FOR EACH ROW
BEGIN
    DECLARE v_presupuesto DECIMAL(18, 2);
    DECLARE v_max_jugadores INT;
    DECLARE v_gasto DECIMAL(18, 2);
    DECLARE v_precio DECIMAL(10, 2);
    DECLARE v_total INT;
    DECLARE v_arquero INT;
    DECLARE v_defensor INT;
    DECLARE v_mediocampista INT;
    DECLARE v_delantero INT;
    DECLARE v_posicion VARCHAR(30);

    SET v_max_jugadores = 20;

    SELECT Presupuesto
    INTO v_presupuesto
    FROM Plantilla
    WHERE IdPlantilla = NEW.IdPlantilla;

    IF v_presupuesto IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La plantilla no existe.';
    END IF;

    SELECT Precio, pos.Nombre
    INTO v_precio, v_posicion
    FROM Jugador j
    LEFT JOIN Posicion pos ON pos.IdPosicion = j.IdPosicion
    WHERE j.IdJugador = NEW.IdJugador;

    IF v_posicion IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El jugador debe tener una posicion valida.';
    END IF;

    SELECT COUNT(*), COALESCE(SUM(j.Precio), 0)
    INTO v_total, v_gasto
    FROM PlantillaJugador pj
    INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
    WHERE pj.IdPlantilla = NEW.IdPlantilla;

    IF v_total >= v_max_jugadores THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La plantilla alcanzo el maximo fijo de 20 jugadores.';
    END IF;

    IF v_gasto + v_precio > v_presupuesto THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La cotizacion de los jugadores supera el presupuesto de la plantilla.';
    END IF;

    IF NEW.EsSuplente = 0 THEN
        SELECT
            COALESCE(SUM(pos.Nombre = 'Arquero'), 0),
            COALESCE(SUM(pos.Nombre = 'Defensor'), 0),
            COALESCE(SUM(pos.Nombre = 'Mediocampista'), 0),
            COALESCE(SUM(pos.Nombre = 'Delantero'), 0)
        INTO v_arquero, v_defensor, v_mediocampista, v_delantero
        FROM PlantillaJugador pj
        INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
        INNER JOIN Posicion pos ON pos.IdPosicion = j.IdPosicion
        WHERE pj.IdPlantilla = NEW.IdPlantilla
          AND pj.EsSuplente = 0;

        IF (v_posicion = 'Arquero' AND v_arquero >= 1)
            OR (v_posicion = 'Defensor' AND v_defensor >= 4)
            OR (v_posicion = 'Mediocampista' AND v_mediocampista >= 4)
            OR (v_posicion = 'Delantero' AND v_delantero >= 2) THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'La formacion titular admite 1 arquero, 4 defensores, 4 mediocampistas y 2 delanteros.';
        END IF;
    END IF;
END//

DROP TRIGGER IF EXISTS TR_PlantillaJugador_ValidarUpdate//
CREATE TRIGGER TR_PlantillaJugador_ValidarUpdate
BEFORE UPDATE ON PlantillaJugador
FOR EACH ROW
BEGIN
    DECLARE v_presupuesto DECIMAL(18, 2);
    DECLARE v_max_jugadores INT;
    DECLARE v_gasto DECIMAL(18, 2);
    DECLARE v_precio DECIMAL(10, 2);
    DECLARE v_total INT;
    DECLARE v_arquero INT;
    DECLARE v_defensor INT;
    DECLARE v_mediocampista INT;
    DECLARE v_delantero INT;
    DECLARE v_posicion VARCHAR(30);

    SET v_max_jugadores = 20;

    SELECT Presupuesto
    INTO v_presupuesto
    FROM Plantilla
    WHERE IdPlantilla = NEW.IdPlantilla;

    IF v_presupuesto IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La plantilla no existe.';
    END IF;

    SELECT Precio, pos.Nombre
    INTO v_precio, v_posicion
    FROM Jugador j
    LEFT JOIN Posicion pos ON pos.IdPosicion = j.IdPosicion
    WHERE j.IdJugador = NEW.IdJugador;

    IF v_posicion IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El jugador debe tener una posicion valida.';
    END IF;

    SELECT COUNT(*), COALESCE(SUM(j.Precio), 0)
    INTO v_total, v_gasto
    FROM PlantillaJugador pj
    INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
    WHERE pj.IdPlantilla = NEW.IdPlantilla
      AND NOT (pj.IdPlantilla = OLD.IdPlantilla AND pj.IdJugador = OLD.IdJugador);

    IF v_total >= v_max_jugadores THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La plantilla alcanzo el maximo fijo de 20 jugadores.';
    END IF;

    IF v_gasto + v_precio > v_presupuesto THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La cotizacion de los jugadores supera el presupuesto de la plantilla.';
    END IF;

    IF NEW.EsSuplente = 0 THEN
        SELECT
            COALESCE(SUM(pos.Nombre = 'Arquero'), 0),
            COALESCE(SUM(pos.Nombre = 'Defensor'), 0),
            COALESCE(SUM(pos.Nombre = 'Mediocampista'), 0),
            COALESCE(SUM(pos.Nombre = 'Delantero'), 0)
        INTO v_arquero, v_defensor, v_mediocampista, v_delantero
        FROM PlantillaJugador pj
        INNER JOIN Jugador j ON j.IdJugador = pj.IdJugador
        INNER JOIN Posicion pos ON pos.IdPosicion = j.IdPosicion
        WHERE pj.IdPlantilla = NEW.IdPlantilla
          AND pj.EsSuplente = 0
          AND NOT (pj.IdPlantilla = OLD.IdPlantilla AND pj.IdJugador = OLD.IdJugador);

        IF (v_posicion = 'Arquero' AND v_arquero >= 1)
            OR (v_posicion = 'Defensor' AND v_defensor >= 4)
            OR (v_posicion = 'Mediocampista' AND v_mediocampista >= 4)
            OR (v_posicion = 'Delantero' AND v_delantero >= 2) THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'La formacion titular admite 1 arquero, 4 defensores, 4 mediocampistas y 2 delanteros.';
        END IF;
    END IF;
END//

DROP TRIGGER IF EXISTS TR_Puntuacion_ValidarInsert//
CREATE TRIGGER TR_Puntuacion_ValidarInsert
BEFORE INSERT ON Puntuacion
FOR EACH ROW
BEGIN
    IF NEW.Fecha < 1 OR NEW.Fecha >= 50 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La fecha del torneo debe ser un entero entre 1 y 49.';
    END IF;

    IF NEW.Puntaje < 1 OR NEW.Puntaje > 10 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El puntaje debe estar entre 1 y 10.';
    END IF;
END//

DROP TRIGGER IF EXISTS TR_Puntuacion_ValidarUpdate//
CREATE TRIGGER TR_Puntuacion_ValidarUpdate
BEFORE UPDATE ON Puntuacion
FOR EACH ROW
BEGIN
    IF NEW.Fecha < 1 OR NEW.Fecha >= 50 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La fecha del torneo debe ser un entero entre 1 y 49.';
    END IF;

    IF NEW.Puntaje < 1 OR NEW.Puntaje > 10 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El puntaje debe estar entre 1 y 10.';
    END IF;
END//

DELIMITER ;
