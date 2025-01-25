IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TipoAgenteFinanceiro' AND xtype='U')
BEGIN
    CREATE TABLE TipoAgenteFinanceiro (
        Id          INT PRIMARY KEY IDENTITY(1,1),
        Descricao   NVARCHAR(100) NOT NULL UNIQUE
    );
END;
