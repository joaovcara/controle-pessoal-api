using Core.V1.Cadastros.Usuario.Models;

namespace Core.V1.Cadastros.Usuario.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<int> AddAsync(UsuarioRequest usuarioRequest);
        Task<int> UpdateAsync(int id, UsuarioRequest usuarioRequest);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<UsuarioModel>> GetAllAsync();
        Task<UsuarioModel> GetByIdAsync(int id);
        Task<int> UpdatePasswordAsync(int id, string password);
        Task<string?> GeneratePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
