USE GranDT;

-- Cambiar la contrasena de ejemplo antes de ejecutar este script.
CREATE USER IF NOT EXISTS 'grandt_app'@'localhost'
    IDENTIFIED BY 'CAMBIAR_POR_UNA_CLAVE_SEGURA';

GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE
    ON GranDT.*
    TO 'grandt_app'@'localhost';

FLUSH PRIVILEGES;
