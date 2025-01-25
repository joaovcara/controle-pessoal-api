IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Sistema')
BEGIN
    CREATE DATABASE Sistema;
    PRINT 'Banco de dados Sistema criado com sucesso.';
END
ELSE
BEGIN
    PRINT 'O banco de dados Sistema já existe.';
END;