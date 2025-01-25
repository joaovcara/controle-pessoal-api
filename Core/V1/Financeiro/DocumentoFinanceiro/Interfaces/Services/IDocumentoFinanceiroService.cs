using Core.V1.Financeiro.DocumentoFinanceiro.Models;

namespace Core.V1.Financeiro.DocumentoFinanceiro.Interfaces.Services
{
    public interface IDocumentoFinanceiroService
    {        
        Task<int> AddAsync(DocumentoFinanceiroModel documentoFinanceiro);
        Task<int> UpdateAsync(int id, DocumentoFinanceiroModel documentoFinanceiro);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<DocumentoFinanceiroModel>> GetAllAsync(int usuarioId, string tipoReceitaDespesa);
        Task<DocumentoFinanceiroModel> GetByIdAsync(int id, int usuarioId);
        Task<int> PagamentoAsync(int id, DocumentoFinanceiroModel documentoFinanceiro);
    }
}
