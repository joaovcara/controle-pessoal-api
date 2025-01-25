using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Core.V1.Financeiro.AgenteFinanceiro.Models;
using Core.V1.Financeiro.AgenteFinanceiro.Interfaces.Repositories;

namespace Core.V1.Financeiro.AgenteFinanceiro.Repositories
{
    public class AgenteFinanceiroRepository : IAgenteFinanceiroRepository
    {
        private readonly string _connectionString;
        const string _databaseName = "dbo.AgenteFinanceiro";

        public AgenteFinanceiroRepository(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada ou está vazia nas configurações.");
        }

        public async Task<int> AddAsync(AgenteFinanceiroModel agenteFinanceiro)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"INSERT INTO {_databaseName} (Descricao, TipoAgenteFinanceiroId, BancoId, Agencia, DigitoAgencia, Conta, DigitoConta, ComputaSaldo)
                             VALUES (@Descricao, @TipoAgenteFinanceiroId, @BancoId, @Agencia, @DigitoAgencia, @Conta, @DigitoConta, @ComputaSaldo)";
                return await db.ExecuteAsync(sql, agenteFinanceiro);
            }
        }

        public async Task<int> UpdateAsync(int id, AgenteFinanceiroModel agenteFinanceiro)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"UPDATE {_databaseName} 
                             SET Descricao = @Descricao,
                                 TipoAgenteFinanceiroId = @TipoAgenteFinanceiroId,
                                 BancoId = @BancoId,
                                 Agencia = @Agencia,
                                 DigitoAgencia = @DigitoAgencia,
                                 Conta = @Conta,
                                 DigitoConta = @DigitoConta,
                                 ComputaSaldo = @ComputaSaldo
                             WHERE Id = @Id";
                return await db.ExecuteAsync(sql, new { agenteFinanceiro.Descricao, agenteFinanceiro.TipoAgenteFinanceiroId, agenteFinanceiro.BancoId, agenteFinanceiro.Agencia, agenteFinanceiro.DigitoAgencia, agenteFinanceiro.Conta, agenteFinanceiro.DigitoConta, agenteFinanceiro.ComputaSaldo, Id = id });
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"DELETE FROM {_databaseName} WHERE Id = @Id";
                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<AgenteFinanceiroModel> GetByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"SELECT * FROM {_databaseName} WHERE Id = @Id";
                var result = await db.QueryFirstOrDefaultAsync<AgenteFinanceiroModel>(sql, new { Id = id });
                if (result == null)
                {
                    throw new KeyNotFoundException($"Agente Financeiro com Id {id} não encontrado.");
                }
                return result;
            }
        }

        public async Task<IEnumerable<AgenteFinanceiroModel>> GetAllAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"SELECT * FROM {_databaseName}";
                return await db.QueryAsync<AgenteFinanceiroModel>(sql);
            }
        }
    }
}