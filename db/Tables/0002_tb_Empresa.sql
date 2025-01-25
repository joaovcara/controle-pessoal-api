IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Empresa' AND xtype='U')
BEGIN
    CREATE TABLE Empresa (
        Id              INT PRIMARY KEY IDENTITY(1,1),
        RazaoSocial     NVARCHAR(255) NOT NULL,
        NomeFantasia    NVARCHAR(255) NOT NULL,
        CNPJ            NVARCHAR(14) NOT NULL UNIQUE,
        Endereco        NVARCHAR(200) NOT NULL,
        Numero          NVARCHAR(10) NOT NULL,
        Bairro          NVARCHAR(200) NOT NULL,
        Cidade          NVARCHAR(200) NOT NULL,
        Estado          CHAR(2) NOT NULL,
        Telefone        NVARCHAR(11),
        Email           NVARCHAR(200),
        DataCadastro    DATETIME NOT NULL,
        CaminhoLogo     NVARCHAR(255)
    );
END;