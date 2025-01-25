using Projeto.Api.Core.V1.Financeiro.Conta.Models;

namespace Core.V1.Financeiro.Conta.Interfaces.Repositories
{
    public interface IBancoRepository
    {
        Task<int> AddAsync(BancoModel banco);
        Task<int> UpdateAsync(int id, BancoModel banco);
        Task<int> DeleteAsync(int id);
        Task<BancoModel> GetByIdAsync(int id);
        Task<IEnumerable<BancoModel>> GetAllAsync();
    }
}