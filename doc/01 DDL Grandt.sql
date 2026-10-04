DROP SCHEMA IF EXISTS GranDT;
CREATE SCHEMA GranDT;
USE GranDT;

-- =========================================
-- 1. EQUIPO
-- =========================================
CREATE TABLE Equipo (
    IdEquipo INT AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    
    -- PK
    CONSTRAINT PK_Equipo PRIMARY KEY (IdEquipo)
);

-- =========================================
-- 2. POSICION 
-- =========================================
CREATE TABLE Posicion (
    IdPosicion INT AUTO_INCREMENT,
    Nombre VARCHAR(30) NOT NULL UNIQUE,
    
    -- PK
    CONSTRAINT PK_Posicion PRIMARY KEY (IdPosicion),
    CONSTRAINT CK_Posicion_Nombre CHECK (Nombre IN ('Arquero', 'Defensor', 'Mediocampista', 'Delantero'))
);

-- Insertar las posiciones básicas iniciales
INSERT INTO Posicion (Nombre) VALUES ('Arquero'), ('Defensor'), ('Mediocampista'), ('Delantero');

-- =========================================
-- 3. JUGADOR (Futbolista)
-- =========================================
CREATE TABLE Jugador (
    IdJugador INT AUTO_INCREMENT,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Apodo VARCHAR(50),
    Precio DECIMAL(10, 2) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    IdPosicion INT, 
    IdEquipo INT,
    
    -- PK
    CONSTRAINT PK_Jugador PRIMARY KEY (IdJugador),
    
    -- FK (Conexiones)
    CONSTRAINT FK_Jugador_Posicion FOREIGN KEY (IdPosicion) REFERENCES Posicion(IdPosicion) ON DELETE SET NULL,
    CONSTRAINT FK_Jugador_Equipo FOREIGN KEY (IdEquipo) REFERENCES Equipo(IdEquipo) ON DELETE SET NULL
);

-- =========================================
-- 4. PLANTILLA
-- =========================================
CREATE TABLE Plantilla (
    IdPlantilla VARCHAR(50),
    Presupuesto DECIMAL(10, 2) NOT NULL,
    
    -- PK
    CONSTRAINT PK_Plantilla PRIMARY KEY (IdPlantilla)
);

-- Configuracion compartida por todas las plantillas.
CREATE TABLE Configuracion (
    IdConfiguracion TINYINT NOT NULL,
    PresupuestoMaximo DECIMAL(10, 2) NOT NULL,
    CantidadMaximaJugadores TINYINT UNSIGNED NOT NULL DEFAULT 20,

    CONSTRAINT PK_Configuracion PRIMARY KEY (IdConfiguracion),
    CONSTRAINT CK_Configuracion_Id CHECK (IdConfiguracion = 1),
    CONSTRAINT CK_Configuracion_Presupuesto CHECK (PresupuestoMaximo >= 0),
    CONSTRAINT CK_Configuracion_Cantidad CHECK (CantidadMaximaJugadores >= 11)
);

INSERT INTO Configuracion (IdConfiguracion, PresupuestoMaximo, CantidadMaximaJugadores)
VALUES (1, 99999999.99, 20);

-- =========================================
-- 5. USUARIO
-- =========================================
CREATE TABLE Usuario (
    Email VARCHAR(100),
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Nacimiento DATE NOT NULL,
    Password CHAR(64) NOT NULL,
    EsAdministrador BIT NOT NULL DEFAULT 0,
    IdPlantilla VARCHAR(50) UNIQUE,

    -- PK
    CONSTRAINT PK_Usuario PRIMARY KEY (Email),
    
    -- FK (Conexiones)
    CONSTRAINT FK_Usuario_Plantilla FOREIGN KEY (IdPlantilla) REFERENCES Plantilla(IdPlantilla) ON DELETE SET NULL
);

-- =========================================
-- 6. PLANTILLA JUGADOR
-- =========================================
CREATE TABLE PlantillaJugador (
    IdPlantilla VARCHAR(50) NOT NULL,
    IdJugador INT NOT NULL,
    Numero INT NOT NULL,
    EsSuplente BIT NOT NULL,

    -- PK (Compuesta)
    CONSTRAINT PK_PlantillaJugador PRIMARY KEY (IdPlantilla, IdJugador),

    -- FK (Conexiones)
    CONSTRAINT FK_PJ_Plantilla FOREIGN KEY (IdPlantilla) REFERENCES Plantilla(IdPlantilla) ON DELETE CASCADE,
    CONSTRAINT FK_PJ_Jugador FOREIGN KEY (IdJugador) REFERENCES Jugador(IdJugador) ON DELETE CASCADE
);

-- =========================================
-- 7. PUNTUACION
-- =========================================
CREATE TABLE Puntuacion (
    IdPuntuacion VARCHAR(50),
    Fecha TINYINT UNSIGNED NOT NULL DEFAULT 1,
    Partido_date DATE NULL,
    Puntaje DECIMAL(3, 1) NOT NULL,
    IdJugador INT NOT NULL,

    -- PK
    CONSTRAINT PK_Puntuacion PRIMARY KEY (IdPuntuacion),
    
    -- FK (Conexiones)
    CONSTRAINT FK_Puntuacion_Jugador FOREIGN KEY (IdJugador) REFERENCES Jugador(IdJugador) ON DELETE CASCADE,
    CONSTRAINT UQ_Puntuacion_Jugador_Fecha UNIQUE (IdJugador, Fecha),
    CONSTRAINT CK_Puntuacion_Fecha CHECK (Fecha BETWEEN 1 AND 49),
    CONSTRAINT CK_Puntuacion_Puntaje CHECK (Puntaje BETWEEN 1 AND 10)
);