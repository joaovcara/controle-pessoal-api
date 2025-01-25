IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DocumentoFinanceiro' AND xtype='U')
BEGIN
    CREATE TABLE DocumentoFinanceiro (
        Id                     INT PRIMARY KEY IDENTITY(1,1),
        DataDocumento          DATETIME      NOT NULL,        
        NumeroDocumento        NVARCHAR(100) NOT NULL,
        Descricao              VARCHAR(200)  NOT NULL,
        IdCategoria            INT           NOT NULL,
        Valor                  DECIMAL(14,2) NOT NULL,
        DataVencimento         DATETIME      NOT NULL, 
        DataPagamento          DATETIME,
        UsuarioId              INT           NOT NULL, 
        CONSTRAINT CK_DocumentoFinanceiro_Valor CHECK (Valor <> 0),
        CONSTRAINT FK_DocumentoFinanceiro_Categoria FOREIGN KEY (IdCategoria) REFERENCES Categoria(Id),
        CONSTRAINT FK_DocumentoFinanceiro_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id) 
    );
END;