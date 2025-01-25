IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pessoa' AND xtype='U')
BEGIN
    CREATE TABLE Pessoa (
        Id              INT PRIMARY KEY IDENTITY(1,1),
        Nome            NVARCHAR(255) NOT NULL,
        CPF             NVARCHAR(14),
        Endereco        NVARCHAR(200) NOT NULL,
        Numero          NVARCHAR(10) NOT NULL,
        Bairro          NVARCHAR(200) NOT NULL,
        Cidade          NVARCHAR(200) NOT NULL,
        Estado          CHAR(2) NOT NULL,
        DataNascimento  DATE,       
        Telefone        NVARCHAR(11),
        Email           NVARCHAR(200),
        DataCadastro    DATE NOT NULL,
        Ativo           BIT
    );
END;