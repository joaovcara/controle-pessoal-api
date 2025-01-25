
using System.Data;
using System.Data.SqlClient;
using Core.V1.Financeiro.Banco.Interfaces.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Core.V1.Financeiro.Banco.Models;

namespace Core.V1.Financeiro.Banco.Repositories
{
    public class BancoRepository : IBancoRepository
    {
        private readonly string _connectionString;
        const string _databaseName = "dbo.Banco";

        public BancoRepository(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada ou está vazia nas configurações.");
        }

        public async Task<int> AddAsync(BancoModel banco)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"INSERT INTO {_databaseName} (Codigo,
                                                          Descricao, 
                                                          NMLogo)
                                  VALUES (@Codigo,
                                          @Descricao, 
                                          @NMLogo)";
                return await db.ExecuteAsync(sql, banco);
            }
        }

        public async Task<int> UpdateAsync(int id, BancoModel banco)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"UPDATE {_databaseName} 
                                SET Codigo = @Codigo, 
                                    Descricao = @Descricao,
                                    NMLogo = @NMLogo
                              WHERE Id = @Id";
                return await db.ExecuteAsync(sql, banco);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"DELETE FROM {_databaseName} 
                                   WHERE Id = @Id";
                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<BancoModel>> GetAllAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT *
                               FROM {_databaseName}";
                return await db.QueryAsync<BancoModel>(query);
            }
        }

        public async Task<BancoModel> GetByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT * FROM {_databaseName} WHERE Id = @Id";
                var banco = await db.QueryFirstOrDefaultAsync<BancoModel>(query, new { Id = id });

                if (banco == null)
                    throw new KeyNotFoundException($"Registro com Id {id} não encontrado na base de dados {_databaseName}.");

                return banco;
            }
        }
    }
}