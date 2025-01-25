IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FormaPagamento' AND xtype='U')
BEGIN
    CREATE TABLE FormaPagamento (
        Id                     INT PRIMARY KEY IDENTITY(1,1),
        Descricao              NVARCHAR(255) NOT NULL UNIQUE,
        DataCadastro           DATETIME NOT NULL,
        Ativo                  BIT
    );
END;
