using Microsoft.Extensions.Configuration;
using Core.V1.Financeiro.DocumentoFinanceiro.Interfaces.Repositories;
using Core.V1.Financeiro.DocumentoFinanceiro.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Core.V1.Financeiro.DocumentoFinanceiro.Repositories
{
    public class DocumentoFinanceiroRepository : IDocumentoFinanceiroRepository
    {
        private readonly string _connectionString;
        const string _databaseName = "dbo.DocumentoFinanceiro";

        public DocumentoFinanceiroRepository(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada ou está vazia nas configurações.");
        }

        public async Task<int> AddAsync(DocumentoFinanceiroModel documentoFinanceiro)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"INSERT INTO {_databaseName} (DataDocumento, 
                                                          NumeroDocumento,
                                                          Descricao,
                                                          IdCategoria,
                                                          Valor,
                                                          DataVencimento,
                                                          DataPagamento, 
                                                          UsuarioId)
                                  VALUES (@DataDocumento, 
                                          @NumeroDocumento,
                                          @Descricao,
                                          @IdCategoria,
                                          @Valor,
                                          @DataVencimento,
                                          @DataPagamento, 
                                          @UsuarioId)";
                return await db.ExecuteAsync(sql, documentoFinanceiro);
            }
        }

        public async Task<int> UpdateAsync(int id, DocumentoFinanceiroModel documentoFinanceiro)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"UPDATE {_databaseName} 
                                SET DataDocumento = @DataDocumento, 
                                    NumeroDocumento = @NumeroDocumento,
                                    Descricao = @Descricao,
                                    IdCategoria = @IdCategoria,
                                    Valor = @Valor,
                                    DataVencimento = @DataVencimento,
                                    DataPagamento = @DataPagamento,
                                    UsuarioId = @UsuarioId
                              WHERE Id = @Id";
                return await db.ExecuteAsync(sql, documentoFinanceiro);
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

        public async Task<IEnumerable<DocumentoFinanceiroModel>> GetAllAsync(int usuarioId, string tipoReceitaDespesa)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT DocumentoFinanceiro.*, 
                                      Categoria.Descricao AS DescricaoCategoria,
                                      Categoria.Cor AS CorCategoria,
                                      Categoria.Icone AS IconeCategoria
                               FROM {_databaseName}
                               INNER JOIN Categoria ON DocumentoFinanceiro.IdCategoria = Categoria.Id
                               INNER JOIN TipoReceitaDespesa ON TipoReceitaDespesa.Id = Categoria.IdTipoReceitaDespesa
                               WHERE TipoReceitaDespesa.Descricao = @TipoReceitaDespesa
                               AND DocumentoFinanceiro.UsuarioId = @UsuarioId";
                return await db.QueryAsync<DocumentoFinanceiroModel>(query, new { TipoReceitaDespesa = tipoReceitaDespesa, UsuarioId = usuarioId });
            }
        }

        public async Task<DocumentoFinanceiroModel> GetByIdAsync(int id, int usuarioId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT * FROM {_databaseName} WHERE Id = @Id AND UsuarioId = @UsuarioId";
                var documentoFinanceiro = await db.QueryFirstOrDefaultAsync<DocumentoFinanceiroModel>(query, new { Id = id, UsuarioId = usuarioId });

                if (documentoFinanceiro == null)
                    throw new KeyNotFoundException($"Registro com Id {id} não encontrado na base de dados {_databaseName}.");

                return documentoFinanceiro;
            }
        }

        public async Task<int> PagamentoAsync(int id, DocumentoFinanceiroModel documentoFinanceiro)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"UPDATE {_databaseName} 
                             SET DataPagamento = @DataPagamento,
                                 Valor = @Valor                                    
                             WHERE Id = @Id AND UsuarioId = @UsuarioId";
                return await db.ExecuteAsync(sql, new { Id = id, 
                                                        DataPagamento = documentoFinanceiro.DataPagamento,
                                                        Valor = documentoFinanceiro.Valor, 
                                                        UsuarioId = documentoFinanceiro.UsuarioId });
            }
        }
    }
}