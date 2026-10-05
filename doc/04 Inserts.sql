-- 1. EQUIPO
INSERT INTO Equipo (Nombre) VALUES 
('Boca Juniors'),
('River Plate');

-- 2. JUGADOR 
-- (Cambiamos la columna "Posicion" por "IdPosicion")
-- (IdPosicion 1 corresponde a 'Arquero' según la tabla Posicion)
INSERT INTO Jugador (Nombre, Apellido, Apodo, Precio, FechaNacimiento, IdPosicion, IdEquipo) VALUES 
('Sergio', 'Romero', 'Chiquito', 2500000.50, '1987-02-22', 1, 1),
('Franco', 'Armani', 'Pulpo', 3100000.00, '1986-10-16', 1, 2);

INSERT INTO Jugador (Nombre, Apellido, Apodo, Precio, FechaNacimiento, IdPosicion, IdEquipo) VALUES
('Marcos', 'Rojo', NULL, 1800000.00, '1990-03-20', 2, 1),
('Nicolas', 'Figal', NULL, 1600000.00, '1994-04-03', 2, 1),
('Lautaro', 'Blanco', NULL, 1400000.00, '1999-02-19', 2, 1),
('Luis', 'Advincula', NULL, 1900000.00, '1990-03-02', 2, 1),
('Pol', 'Fernandez', NULL, 2200000.00, '1991-01-23', 3, 1),
('Cristian', 'Medina', NULL, 2500000.00, '2002-06-01', 3, 1),
('Ezequiel', 'Fernandez', NULL, 2300000.00, '2002-07-25', 3, 1),
('Kevin', 'Zenon', NULL, 3000000.00, '2001-07-30', 3, 1),
('Miguel', 'Merentiel', NULL, 4000000.00, '1996-02-24', 4, 1),
('Edinson', 'Cavani', NULL, 3500000.00, '1987-02-14', 4, 1),
('Paulo', 'Diaz', NULL, 3200000.00, '1994-08-25', 2, 2),
('German', 'Pezzella', NULL, 3000000.00, '1991-06-27', 2, 2),
('Enzo', 'Diaz', NULL, 2200000.00, '1995-12-07', 2, 2),
('Milton', 'Casco', NULL, 1800000.00, '1988-04-11', 2, 2),
('Rodrigo', 'Aliendro', NULL, 2100000.00, '1991-02-16', 3, 2),
('Nicolas', 'De La Cruz', NULL, 4500000.00, '1997-06-01', 3, 2),
('Nacho', 'Fernandez', NULL, 2000000.00, '1990-01-12', 3, 2),
('Manuel', 'Lanzini', NULL, 1900000.00, '1993-02-15', 3, 2),
('Miguel', 'Borja', NULL, 3800000.00, '1993-01-26', 4, 2),
('Facundo', 'Colidio', NULL, 3200000.00, '2000-01-04', 4, 2);

-- 3. PLANTILLA
INSERT INTO Plantilla (IdPlantilla, Presupuesto) VALUES 
('PLANT-ARG-001', 99999999.99),
('PLANT-ARG-002', 99999999.99);

-- 4. USUARIO
INSERT INTO Usuario (Email, Nombre, Apellido, Nacimiento, Password, EsAdministrador, IdPlantilla) VALUES 
('mateo.caba@email.com', 'Mateo', 'Rossi', '2004-05-14', SHA2('bocacampeon123', 256), 0, 'PLANT-ARG-001'),
('lucas.mza@email.com', 'Lucas', 'Fernandez', '2001-11-25', SHA2('millonario987', 256), 0, 'PLANT-ARG-002');

-- 5. PLANTILLA JUGADOR
INSERT INTO PlantillaJugador (IdPlantilla, IdJugador, Numero, EsSuplente) VALUES 
('PLANT-ARG-001', 1, 1, 0),
('PLANT-ARG-002', 2, 1, 0),
('PLANT-ARG-001', 3, 2, 0),
('PLANT-ARG-001', 4, 3, 0),
('PLANT-ARG-001', 5, 4, 0),
('PLANT-ARG-001', 6, 5, 0),
('PLANT-ARG-001', 7, 6, 0),
('PLANT-ARG-001', 8, 7, 0),
('PLANT-ARG-001', 9, 8, 0),
('PLANT-ARG-001', 11, 10, 0),
('PLANT-ARG-001', 12, 11, 0),
('PLANT-ARG-002', 13, 2, 0),
('PLANT-ARG-002', 14, 3, 0),
('PLANT-ARG-002', 15, 4, 0),
('PLANT-ARG-002', 16, 5, 0),
('PLANT-ARG-002', 17, 6, 0),
('PLANT-ARG-002', 18, 7, 0),
('PLANT-ARG-002', 19, 8, 0),
('PLANT-ARG-002', 21, 10, 0),
('PLANT-ARG-002', 22, 11, 0);

-- 6. PUNTUACION
INSERT INTO Puntuacion (IdPuntuacion, Fecha, Partido_date, Puntaje, IdJugador) VALUES 
('PUNT-FECHA1-01', 1, '2026-09-28', 8.0, 1),
('PUNT-FECHA1-02', 1, '2026-09-28', 10.0, 2);