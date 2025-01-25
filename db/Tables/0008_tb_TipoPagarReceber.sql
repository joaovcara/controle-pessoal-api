IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TipoReceitaDespesa' AND xtype='U')
BEGIN
    CREATE TABLE TipoReceitaDespesa (
        Id                     INT PRIMARY KEY IDENTITY(1,1),
        Descricao              NVARCHAR(255) NOT NULL UNIQUE
    );
END;
