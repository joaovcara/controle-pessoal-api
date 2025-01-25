IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AgenteFinanceiro' AND xtype='U')
BEGIN
    CREATE TABLE AgenteFinanceiro (
        Id                     INT PRIMARY KEY IDENTITY(1,1),
        Descricao              VARCHAR(200) NOT NULL,
        TipoAgenteFinanceiroId INT NOT NULL,
        BancoId                INT,
        Agencia                INT,
        DigitoAgencia          INT,
        Conta                  INT,
        DigitoConta            INT,
        ComputaSaldo           BIT

        CONSTRAINT FK_AgenteFinanceiro_TipoAgenteFinanceiro FOREIGN KEY (TipoAgenteFinanceiroId) REFERENCES TipoAgenteFinanceiro(Id),
        CONSTRAINT FK_AgenteFinanceiro_Banco FOREIGN KEY (BancoId) REFERENCES Banco(Id)
    );
END;