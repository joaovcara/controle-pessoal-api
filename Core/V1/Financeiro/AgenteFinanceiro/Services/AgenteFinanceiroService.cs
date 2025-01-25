using Core.V1.Financeiro.AgenteFinanceiro.Interfaces.Repositories;
using Core.V1.Financeiro.AgenteFinanceiro.Interfaces.Services;
using Core.V1.Financeiro.AgenteFinanceiro.Models;

namespace Core.V1.Financeiro.AgenteFinanceiro.Services
{
    public class AgenteFinanceiroService : IAgenteFinanceiroService
    {
        private readonly IAgenteFinanceiroRepository _agenteFinanceiroRepository;

        public AgenteFinanceiroService(IAgenteFinanceiroRepository agenteFinanceiroRepository)
        {
            _agenteFinanceiroRepository = agenteFinanceiroRepository;
        }

        public async Task<int> AddAsync(AgenteFinanceiroModel agenteFinanceiro)
        {
            return await _agenteFinanceiroRepository.AddAsync(agenteFinanceiro);
        }

        public async Task<int> UpdateAsync(int id, AgenteFinanceiroModel agenteFinanceiro)
        {
            return await _agenteFinanceiroRepository.UpdateAsync(id, agenteFinanceiro);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _agenteFinanceiroRepository.DeleteAsync(id);
        }

        public async Task<AgenteFinanceiroModel> GetByIdAsync(int id)
        {
            return await _agenteFinanceiroRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AgenteFinanceiroModel>> GetAllAsync()
        {
            return await _agenteFinanceiroRepository.GetAllAsync();
        }
    }
}