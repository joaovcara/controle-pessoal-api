using Core.V1.Cadastros.Usuario.Models;
using Core.V1.Cadastros.Usuario.Interfaces.Repositories;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Core.Utils;

namespace Core.V1.Cadastros.Usuario.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        const string _databaseName = "dbo.Usuario";

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada ou está vazia nas configurações.");
        }

        public async Task<int> AddAsync(UsuarioRequest usuarioRequest)
        {
            // Gere a senha criptografada e o salt
            var passwordData = CriptografaSenha.GeneratedPasswordCripto(usuarioRequest.Usuario);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                UsuarioModel usuario = new UsuarioModel
                {
                    Nome = usuarioRequest.Nome,
                    Usuario = usuarioRequest.Usuario,
                    HashSenha = passwordData["PasswordCripto"],
                    Email = usuarioRequest.Email,
                    Salt = passwordData["Salt"],
                    DataCadastro = DateTime.Now,
                    Ativo = true
                };

                var sql = $@"INSERT INTO {_databaseName} (Nome, Usuario, HashSenha, Email, Salt, DataCadastro, Ativo) 
                                  VALUES (@Nome, @Usuario, @HashSenha, @Email, @Salt, @DataCadastro, @Ativo)";
                return await db.ExecuteAsync(sql, usuario);
            }
        }

        public async Task<int> UpdateAsync(int id, UsuarioRequest usuarioRequest)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                UsuarioModel usuario = new UsuarioModel
                {
                    Id = id,
                    Nome = usuarioRequest.Nome,
                    Usuario = usuarioRequest.Usuario,
                    Email = usuarioRequest.Email,
                    Ativo = usuarioRequest.Ativo
                };

                var sql = $@"UPDATE {_databaseName} 
                                SET Nome = @Nome, 
                                    Usuario = @Usuario, 
                                    Email = @Email, 
                                    Ativo = @Ativo 
                              WHERE Id = @Id";
                return await db.ExecuteAsync(sql, usuario);
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

        public async Task<IEnumerable<UsuarioModel>> GetAllAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<UsuarioModel>($@"SELECT * FROM {_databaseName}");
            }
        }

        public async Task<UsuarioModel> GetByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT * FROM {_databaseName} WHERE Id = @Id";
                var usuario = await db.QueryFirstOrDefaultAsync<UsuarioModel>(query, new { Id = id });

                if (usuario == null)
                    throw new KeyNotFoundException($"Registro com Id {id} não encontrado na base de dados {_databaseName}.");

                return usuario;
            }
        }

        public async Task<UsuarioModel> GetUser(string username)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT * FROM {_databaseName} WHERE Usuario = @Username";
                var usuario = await db.QueryFirstOrDefaultAsync<UsuarioModel>(query, new { Username = username });

                if (usuario == null)
                    throw new KeyNotFoundException($" Usuário {username} não encontrado na base de dados {_databaseName}.");

                return usuario;
            }
        }

        public async Task<int> UpdatePasswordAsync(int id, string password)
        {
            // Gere a senha criptografada e o salt
            var passwordData = CriptografaSenha.GeneratedPasswordCripto(password);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                UsuarioModel usuario = new UsuarioModel
                {
                    Id = id,
                    HashSenha = passwordData["PasswordCripto"],
                    Salt = passwordData["Salt"]
                };

                var sql = $@"UPDATE {_databaseName}
                                SET HashSenha = @HashSenha,
                                    Salt = @Salt
                              WHERE Id = @Id";
                return await db.ExecuteAsync(sql, usuario);
            }
        }

        public async Task<UsuarioModel> GetUserByEmailAsync(string email)
        {
            using var db = new SqlConnection(_connectionString);
            var query = $"SELECT * FROM {_databaseName} WHERE Email = @Email";
            return await db.QueryFirstOrDefaultAsync<UsuarioModel>(query, new { Email = email }) 
                ?? throw new KeyNotFoundException($" Usuário com email {email} não encontrado na base de dados {_databaseName}.");
        }

        public async Task<int> UpdatePasswordByEmailAsync(string email, string password)
        {
            var passwordData = CriptografaSenha.GeneratedPasswordCripto(password);

            using var db = new SqlConnection(_connectionString);
            var sql = $"UPDATE {_databaseName} SET HashSenha = @HashSenha, Salt = @Salt WHERE Email = @Email";
            return await db.ExecuteAsync(sql, new { HashSenha = passwordData["PasswordCripto"], Salt = passwordData["Salt"], Email = email });
        }
    }
}
