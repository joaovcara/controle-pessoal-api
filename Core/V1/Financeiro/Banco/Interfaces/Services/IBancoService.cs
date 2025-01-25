using Core.V1.Financeiro.Banco.Models;

namespace Core.V1.Financeiro.Banco.Interfaces.Services
{
    public interface IBancoService
    {
        Task<int> AddAsync(BancoModel banco);
        Task<int> UpdateAsync(int id, BancoModel banco);
        Task<int> DeleteAsync(int id);
        Task<BancoModel> GetByIdAsync(int id);
        Task<IEnumerable<BancoModel>> GetAllAsync();
    }
}