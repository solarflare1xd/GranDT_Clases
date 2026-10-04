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

-- 3. PLANTILLA
INSERT INTO Plantilla (IdPlantilla, Presupuesto) VALUES 
('PLANT-ARG-001', 15000000.00),
('PLANT-ARG-002', 20000000.00);

-- 4. USUARIO
INSERT INTO Usuario (Email, Nombre, Apellido, Nacimiento, Password, EsAdministrador, IdPlantilla) VALUES 
('mateo.caba@email.com', 'Mateo', 'Rossi', '2004-05-14', 'bocacampeon123', 0, 'PLANT-ARG-001'),
('lucas.mza@email.com', 'Lucas', 'Fernandez', '2001-11-25', 'millonario987', 0, 'PLANT-ARG-002');

-- 5. PLANTILLA JUGADOR
INSERT INTO PlantillaJugador (IdPlantilla, IdJugador, Numero, EsSuplente) VALUES 
('PLANT-ARG-001', 1, 1, 0),
('PLANT-ARG-002', 2, 1, 0);

-- 6. PUNTUACION
INSERT INTO Puntuacion (IdPuntuacion, Partido_date, Puntaje, IdJugador) VALUES 
('PUNT-FECHA1-01', '2026-09-28', 8, 1),
('PUNT-FECHA1-02', '2026-09-28', 10, 2);