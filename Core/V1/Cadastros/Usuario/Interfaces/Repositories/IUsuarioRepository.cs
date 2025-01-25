using Core.V1.Cadastros.Usuario.Models;

namespace Core.V1.Cadastros.Usuario.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<int> AddAsync(UsuarioRequest usuarioRequest);
        Task<int> UpdateAsync(int id, UsuarioRequest usuarioRequest);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<UsuarioModel>> GetAllAsync();
        Task<UsuarioModel> GetByIdAsync(int id);
        Task<UsuarioModel> GetUser(string username);
        Task<int> UpdatePasswordAsync(int id, string password);
        Task<UsuarioModel> GetUserByEmailAsync(string email);
        Task<int> UpdatePasswordByEmailAsync(string token, string newPassword);
    }
}
