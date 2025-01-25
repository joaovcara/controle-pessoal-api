IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Estado' AND xtype='U')
BEGIN
    CREATE TABLE Estado (
        Id          INT PRIMARY KEY IDENTITY(1,1),
        Sigla       CHAR(2) NOT NULL,
        Descricao   NVARCHAR(100) NOT NULL
    );
END;
