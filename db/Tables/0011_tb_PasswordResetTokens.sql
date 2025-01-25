IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PasswordResetTokens' AND xtype='U')
BEGIN
  CREATE TABLE PasswordResetTokens (
      Token      VARCHAR(255) PRIMARY KEY,
      Email      VARCHAR(255),
      Expiration DATETIME
  );
END;