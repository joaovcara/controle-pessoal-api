IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Banco' AND xtype='U')
BEGIN
    CREATE TABLE Banco (
        Id                   INT PRIMARY KEY IDENTITY(1,1),
        Codigo               VARCHAR(3) NOT NULL,
        Descricao            VARCHAR(200) NOT NULL,
        NMLogo               VARCHAR(200)
    );
END;