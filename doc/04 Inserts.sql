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
INSERT INTO Plantilla (Presupuesto) VALUES (99999999.99);
SET @IdPlantillaMateo = LAST_INSERT_ID();

INSERT INTO Plantilla (Presupuesto) VALUES (99999999.99);
SET @IdPlantillaLucas = LAST_INSERT_ID();

-- 4. USUARIO
INSERT INTO Usuario (Email, Nombre, Apellido, Nacimiento, Password, EsAdministrador, IdPlantilla) VALUES 
('mateo.caba@email.com', 'Mateo', 'Rossi', '2004-05-14', SHA2('bocacampeon123', 256), 0, @IdPlantillaMateo),
('lucas.mza@email.com', 'Lucas', 'Fernandez', '2001-11-25', SHA2('millonario987', 256), 0, @IdPlantillaLucas);

-- 5. PLANTILLA JUGADOR
INSERT INTO PlantillaJugador (IdPlantilla, IdJugador, Numero, EsSuplente) VALUES 
(@IdPlantillaMateo, 1, 1, 0),
(@IdPlantillaLucas, 2, 1, 0),
(@IdPlantillaMateo, 3, 2, 0),
(@IdPlantillaMateo, 4, 3, 0),
(@IdPlantillaMateo, 5, 4, 0),
(@IdPlantillaMateo, 6, 5, 0),
(@IdPlantillaMateo, 7, 6, 0),
(@IdPlantillaMateo, 8, 7, 0),
(@IdPlantillaMateo, 9, 8, 0),
(@IdPlantillaMateo, 21, 9, 0),
(@IdPlantillaMateo, 11, 10, 0),
(@IdPlantillaMateo, 12, 11, 0),
(@IdPlantillaLucas, 13, 2, 0),
(@IdPlantillaLucas, 14, 3, 0),
(@IdPlantillaLucas, 15, 4, 0),
(@IdPlantillaLucas, 16, 5, 0),
(@IdPlantillaLucas, 17, 6, 0),
(@IdPlantillaLucas, 18, 7, 0),
(@IdPlantillaLucas, 19, 8, 0),
(@IdPlantillaLucas, 11, 9, 0),
(@IdPlantillaLucas, 21, 10, 0),
(@IdPlantillaLucas, 22, 11, 0);

-- 6. PUNTUACION
INSERT INTO Puntuacion (IdPuntuacion, Fecha, Partido_date, Puntaje, IdJugador) VALUES 
('PUNT-FECHA1-01', 1, '2026-09-28', 8.0, 1),
('PUNT-FECHA1-02', 1, '2026-09-28', 10.0, 2);