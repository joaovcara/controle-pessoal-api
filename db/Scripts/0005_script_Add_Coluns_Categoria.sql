IF NOT EXISTS (SELECT * FROM sys.columns 
               WHERE object_id = OBJECT_ID('Categoria') 
               AND name = 'Cor')
BEGIN
    ALTER TABLE Categoria
    ADD Cor VARCHAR(7); -- Armazena a cor no formato hexadecimal
END;

IF NOT EXISTS (SELECT * FROM sys.columns 
               WHERE object_id = OBJECT_ID('Categoria') 
               AND name = 'Icone')
BEGIN
    ALTER TABLE Categoria
    ADD Icone VARCHAR(255); -- Armazena o nome do ícone
END;