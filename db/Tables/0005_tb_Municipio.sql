IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Municipio' AND xtype='U')
BEGIN
    CREATE TABLE Municipio (
        Id 	   INT PRIMARY KEY IDENTITY,
        Codigo INT NOT NULL,
        Nome 	 VARCHAR(255) NOT NULL,
        Uf	   CHAR(2) NOT NULL,
    );
END;