USE GranDT;

-- Cambiar la contrasena de ejemplo antes de ejecutar este script.
CREATE USER IF NOT EXISTS 'Usuario'@'localhost'
    IDENTIFIED BY '123456789Messi';

CREATE USER IF NOT EXISTS 'grandt_app'@'localhost'
    IDENTIFIED BY 'Admin1234';

GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE
    ON GranDT.*
    TO 'grandt_app'@'localhost';

GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE
    ON GranDT.
    TO 'Usuario'@'localhost';

FLUSH PRIVILEGES;
