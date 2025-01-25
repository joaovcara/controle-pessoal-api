IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuario' AND xtype='U')
BEGIN
    CREATE TABLE Usuario (
        Id              INT PRIMARY KEY IDENTITY(1,1),
        Nome            NVARCHAR(255) NOT NULL,
        Usuario         NVARCHAR(50) NOT NULL UNIQUE,
        HashSenha       VARCHAR(200) NOT NULL,
        Email           NVARCHAR(200) NOT NULL UNIQUE,
        Salt            VARCHAR(200) NOT NULL,
        DataCadastro    DATETIME NOT NULL,
        Ativo           BIT
    );
END;