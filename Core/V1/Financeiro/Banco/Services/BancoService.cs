using Core.V1.Financeiro.Banco.Interfaces.Repositories;
using Core.V1.Financeiro.Banco.Interfaces.Services;
using Core.V1.Financeiro.Banco.Models;

namespace Core.V1.Financeiro.Banco.Services
{
    public class BancoService : IBancoService
    {
        private readonly IBancoRepository _bancoRepository;

        public BancoService(IBancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository;
        }

        public async Task<int> AddAsync(BancoModel banco)
        {
            return await _bancoRepository.AddAsync(banco);
        }

        public async Task<int> UpdateAsync(int id, BancoModel banco)
        {
            return await _bancoRepository.UpdateAsync(id, banco);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _bancoRepository.DeleteAsync(id);
        }

        public async Task<BancoModel> GetByIdAsync(int id)
        {
            return await _bancoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BancoModel>> GetAllAsync()
        {
            return await _bancoRepository.GetAllAsync();
        }
    }
}