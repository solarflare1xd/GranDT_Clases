DROP SCHEMA IF EXISTS GranDTviejo;
CREATE SCHEMA GranDTviejo;
USE GranDTviejo;


-- =========================================
-- 1. FUTBOLISTA
-- =========================================

CREATE TABLE Futbolista (
    IdJugador INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Apodo VARCHAR(50),
    Precio FLOAT NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Posicion VARCHAR(30) NOT NULL
);


-- =========================================
-- 2. EQUIPO
-- =========================================

CREATE TABLE Equipo (
    IdEquipo INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL
);


-- =========================================
-- 3. PLANTILLA
-- =========================================

CREATE TABLE Plantilla (
    IdPlantilla VARCHAR(50) PRIMARY KEY,
    Presupuesto FLOAT NOT NULL
);


-- =========================================
-- 4. PUNTACION
-- =========================================

CREATE TABLE Puntacion (
    IdPuntacion VARCHAR(50) PRIMARY KEY,
    IdJugador INT NOT NULL,
    Partido_date DATE NOT NULL,
    Puntaje INT NOT NULL,

    FOREIGN KEY (IdJugador)
        REFERENCES Futbolista(IdJugador)
);


-- =========================================
-- 5. EQUIPO - JUGADOR
-- =========================================

CREATE TABLE EquipoJugador (
    IdEquipo INT NOT NULL,
    IdJugador INT NOT NULL,

    PRIMARY KEY (IdEquipo, IdJugador),

    FOREIGN KEY (IdEquipo)
        REFERENCES Equipo(IdEquipo),

    FOREIGN KEY (IdJugador)
        REFERENCES Futbolista(IdJugador)
);


-- =========================================
-- 6. PLANTILLA - JUGADOR
-- =========================================

CREATE TABLE PlantillaJugador (
    IdPlantilla VARCHAR(50) NOT NULL,
    IdJugador INT NOT NULL,
    Numero INT NOT NULL,
    EsSuplente BIT NOT NULL,

    PRIMARY KEY (IdPlantilla, IdJugador),

    FOREIGN KEY (IdPlantilla)
        REFERENCES Plantilla(IdPlantilla),

    FOREIGN KEY (IdJugador)
        REFERENCES Futbolista(IdJugador)
);


-- =========================================
-- 7. USUARIO
-- =========================================

CREATE TABLE Usuario (
    Email VARCHAR(100) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Nacimiento DATE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    EsAdministrador BIT NOT NULL,
    IdPlantilla VARCHAR(50),

    FOREIGN KEY (IdPlantilla)
        REFERENCES Plantilla(IdPlantilla)
	
);