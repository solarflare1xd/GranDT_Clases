DROP SCHEMA IF EXISTS GranDT;
CREATE SCHEMA GranDT;
USE GranDT;

-- =========================================
-- 1. EQUIPO
-- =========================================
CREATE TABLE Equipo (
    IdEquipo INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL UNIQUE
);

-- =========================================
-- 2. JUGADOR (Futbolista)
-- =========================================
CREATE TABLE Jugador (
    IdJugador INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Apodo VARCHAR(50),
    Precio FLOAT NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Posicion VARCHAR(30) NOT NULL,
    IdEquipo INT,
    
    FOREIGN KEY (IdEquipo) 
        REFERENCES Equipo(IdEquipo) 
        ON DELETE SET NULL
);

-- =========================================
-- 3. PLANTILLA
-- =========================================
CREATE TABLE Plantilla (
    IdPlantilla VARCHAR(50) PRIMARY KEY,
    Presupuesto FLOAT NOT NULL
);

-- =========================================
-- 4. USUARIO
-- =========================================
CREATE TABLE Usuario (
    Email VARCHAR(100) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Nacimiento DATE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    EsAdministrador BIT NOT NULL DEFAULT 0,
    IdPlantilla VARCHAR(50) UNIQUE,

    FOREIGN KEY (IdPlantilla) 
        REFERENCES Plantilla(IdPlantilla) 
        ON DELETE CASCADE
);

-- =========================================
-- 5. PLANTILLA JUGADOR (Relación con atributos extra)
-- =========================================
CREATE TABLE PlantillaJugador (
    IdPlantilla VARCHAR(50) NOT NULL,
    IdJugador INT NOT NULL,
    Numero INT NOT NULL,
    EsSuplente BIT NOT NULL,

    PRIMARY KEY (IdPlantilla, IdJugador),

    FOREIGN KEY (IdPlantilla) 
        REFERENCES Plantilla(IdPlantilla) 
        ON DELETE CASCADE,

    FOREIGN KEY (IdJugador) 
        REFERENCES Jugador(IdJugador) 
        ON DELETE CASCADE
);

-- =========================================
-- 6. PUNTUACION
-- =========================================
CREATE TABLE Puntuacion (
    IdPuntuacion VARCHAR(50) PRIMARY KEY,
    Partido_date DATE NOT NULL,
    Puntaje INT NOT NULL,
    IdJugador INT NOT NULL,

    FOREIGN KEY (IdJugador) 
        REFERENCES Jugador(IdJugador) 
        ON DELETE CASCADE
);

