using Core.V1.Cadastros.Categoria.Interfaces.Repositories;
using Core.V1.Cadastros.Categoria.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Core.V1.Cadastros.Categoria.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _connectionString;
        const string _databaseName = "dbo.Categoria";

        public CategoriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada ou está vazia nas configurações.");
        }

        public async Task<int> AddAsync(CategoriaModel categoria)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"INSERT INTO {_databaseName} (Descricao, IdTipoReceitaDespesa, Ativo, Cor, Icone, UsuarioId) 
                             VALUES (@Descricao, @IdTipoReceitaDespesa, @Ativo, @Cor, @Icone, @UsuarioId)";
                return await db.ExecuteAsync(sql, categoria);
            }
        }

        public async Task<int> UpdateAsync(int id, CategoriaModel categoria)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"UPDATE {_databaseName} 
                             SET Descricao = @Descricao, 
                                 IdTipoReceitaDespesa = @IdTipoReceitaDespesa, 
                                 Ativo = @Ativo,
                                 Cor = @Cor,
                                 Icone = @Icone,
                                 UsuarioId = @UsuarioId 
                             WHERE Id = @Id";
                return await db.ExecuteAsync(sql, new { categoria.Descricao, categoria.IdTipoReceitaDespesa, categoria.Ativo, categoria.Cor, categoria.Icone, categoria.UsuarioId, Id = id });
            }
        }

        public async Task<int> DeleteAsync(int id, int usuarioId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"DELETE FROM {_databaseName} WHERE Id = @Id AND UsuarioId = @UsuarioId";
                return await db.ExecuteAsync(sql, new { Id = id, UsuarioId = usuarioId });
            }
        }

        public async Task<IEnumerable<CategoriaModel>> GetAllAsync(int usuarioId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"SELECT * FROM {_databaseName} WHERE UsuarioId = @UsuarioId";
                return await db.QueryAsync<CategoriaModel>(sql, new { UsuarioId = usuarioId });
            }
        }

        public async Task<CategoriaModel> GetByIdAsync(int id, int usuarioId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT * FROM {_databaseName} WHERE Id = @Id AND UsuarioId = @UsuarioId";
                var categoria = await db.QueryFirstOrDefaultAsync<CategoriaModel>(query, new { Id = id, UsuarioId = usuarioId });

                if (categoria == null)
                    throw new KeyNotFoundException($"Registro com Id {id} não encontrado na base de dados {_databaseName}.");

                return categoria;
            }
        }

        public async Task<IEnumerable<CategoriaModel>> GetAllByTipoReceitaDespesaAsync(string tipoReceitaDespesa, int usuarioId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT Categoria.* 
                               FROM {_databaseName}
                               INNER JOIN TipoReceitaDespesa ON TipoReceitaDespesa.Id = Categoria.IdTipoReceitaDespesa
                               WHERE TipoReceitaDespesa.Descricao = @TipoReceitaDespesa
                               AND Categoria.UsuarioId = @UsuarioId";
                return await db.QueryAsync<CategoriaModel>(query, new { TipoReceitaDespesa = tipoReceitaDespesa, UsuarioId = usuarioId });
            }
        }
    }
}