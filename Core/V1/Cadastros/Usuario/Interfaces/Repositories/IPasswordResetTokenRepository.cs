using Core.V1.Cadastros.Usuario.Models;

namespace Core.V1.Cadastros.Usuario.Interfaces.Repositories
{
    public interface IPasswordResetTokenRepository
    {
        Task<int> AddAsync(PasswordResetToken token);
        Task<PasswordResetToken?> GetByTokenAsync(string token);
        Task<int> DeleteAsync(PasswordResetToken token);
    }
}
