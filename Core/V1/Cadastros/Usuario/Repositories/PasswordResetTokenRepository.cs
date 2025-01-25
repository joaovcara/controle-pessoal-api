using System.Data;
using System.Data.SqlClient;
using Core.V1.Cadastros.Usuario.Interfaces.Repositories;
using Core.V1.Cadastros.Usuario.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly string _connectionString;
    const string _databaseName = "dbo.PasswordResetTokens";

    public PasswordResetTokenRepository(IConfiguration configuration)
    {
        _connectionString = configuration?.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada ou está vazia nas configurações.");
    }

    public async Task<int> AddAsync(PasswordResetToken token)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = $@"INSERT INTO {_databaseName} (Token, Email, Expiration) 
                              VALUES (@Token, @Email, @Expiration)";
            return await db.ExecuteAsync(sql, token);
        }
    }

    public async Task<PasswordResetToken?> GetByTokenAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token não pode ser nulo ou vazio.", nameof(token));

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            // Consulta SQL utilizando parâmetros para evitar problemas com caracteres especiais
            var query = "SELECT * FROM dbo.PasswordResetTokens WHERE Token = @Token";

            // Executa a consulta com o valor do token, sem alterações
            var result = await db.QueryFirstOrDefaultAsync<PasswordResetToken>(query, new { Token = token });

            // Caso não encontre o token, lança uma exceção com mensagem personalizada
            if (result == null)
                throw new KeyNotFoundException($"Registro com token '{token}' não encontrado na base de dados.");

            return result;
        }
    }

    public async Task<int> DeleteAsync(PasswordResetToken token)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = $@"DELETE FROM {_databaseName}
                               WHERE Token = @Token";
            return await db.ExecuteAsync(sql, token);
        }
    }
}
