USE GranDT;

-- Reemplazar ambas contrasenas de ejemplo antes de ejecutar este script.
-- grandt_app es la cuenta administradora de la base del proyecto.
CREATE USER IF NOT EXISTS 'grandt_app'@'localhost'
    IDENTIFIED BY 'REEMPLAZAR_CON_CLAVE_SEGURA_ADMIN';
ALTER USER 'grandt_app'@'localhost'
    IDENTIFIED BY 'REEMPLAZAR_CON_CLAVE_SEGURA_ADMIN';

-- Usuario es la cuenta invitada: solo puede consultar datos.
CREATE USER IF NOT EXISTS 'Usuario'@'localhost'
    IDENTIFIED BY 'REEMPLAZAR_CON_CLAVE_SEGURA_INVITADO';
ALTER USER 'Usuario'@'localhost'
    IDENTIFIED BY 'REEMPLAZAR_CON_CLAVE_SEGURA_INVITADO';

-- Eliminar permisos anteriores para que el resultado sea idempotente y seguro.
REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'grandt_app'@'localhost';
REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'Usuario'@'localhost';

GRANT ALL PRIVILEGES
    ON GranDT.*
    TO 'grandt_app'@'localhost';

GRANT SELECT
    ON GranDT.*
    TO 'Usuario'@'localhost';

FLUSH PRIVILEGES;
