IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categoria' AND xtype='U')
BEGIN
    CREATE TABLE Categoria (
        Id                   INT PRIMARY KEY IDENTITY(1,1),
        Descricao            VARCHAR(200) NOT NULL,
        IdTipoReceitaDespesa INT NOT NULL,
        Ativo                BIT,
        Cor                  VARCHAR(7),
        Icone                VARCHAR(255),
        UsuarioId            INT NOT NULL, 
        CONSTRAINT FK_Categoria_TipoReceitaDespesa FOREIGN KEY (IdTipoReceitaDespesa) REFERENCES TipoReceitaDespesa(Id),
        CONSTRAINT FK_Categoria_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id) 
    );
END;