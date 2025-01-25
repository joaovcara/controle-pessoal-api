using Core.V1.Cadastros.Usuario.Models;
using Core.V1.Cadastros.Usuario.Interfaces.Services;
using Core.V1.Cadastros.Usuario.Interfaces.Repositories;
using Core.Utils;
using System.Security.Cryptography;

namespace Core.V1.Cadastros.Usuario.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IPasswordResetTokenRepository passwordResetTokenRepository)
        {
            _usuarioRepository = usuarioRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
        }

        public async Task<int> AddAsync(UsuarioRequest usuarioRequest)
        {
            return await _usuarioRepository.AddAsync(usuarioRequest);
        }

        public async Task<int> UpdateAsync(int id, UsuarioRequest usuarioRequest)
        {
            return await _usuarioRepository.UpdateAsync(id, usuarioRequest);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<UsuarioModel> GetByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdatePasswordAsync(int id, string password)
        {
            return await _usuarioRepository.UpdatePasswordAsync(id, password);
        }

        public async Task<string?> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _usuarioRepository.GetUserByEmailAsync(email);
            if (user == null)
                return null;

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            // Armazenar o token no banco de dados
            var tokenData = new PasswordResetToken
            {
                Token = token,
                Email = email,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
            await _passwordResetTokenRepository.AddAsync(tokenData);

            return token;
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var tokenData = await _passwordResetTokenRepository.GetByTokenAsync(token);

            if (tokenData == null || tokenData.Email != email || tokenData.Expiration < DateTime.UtcNow)
                return false;

            // Excluir o token após usá-lo
            await _passwordResetTokenRepository.DeleteAsync(tokenData);

            await _usuarioRepository.UpdatePasswordByEmailAsync(email, newPassword);
            return true;
        }
    }
}
