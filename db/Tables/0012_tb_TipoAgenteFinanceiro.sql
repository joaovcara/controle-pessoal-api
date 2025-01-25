IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TipoAgenteFinanceiro' AND xtype='U')
BEGIN
    CREATE TABLE TipoAgenteFinanceiro (
        Id                   INT PRIMARY KEY IDENTITY(1,1),
        Descricao            VARCHAR(200) NOT NULL
    );
END;