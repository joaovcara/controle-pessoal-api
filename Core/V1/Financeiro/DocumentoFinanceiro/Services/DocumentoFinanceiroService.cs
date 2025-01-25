using Core.V1.Cadastros.Categoria.Interfaces.Repositories;
using Core.V1.Financeiro.DocumentoFinanceiro.Interfaces.Repositories;
using Core.V1.Financeiro.DocumentoFinanceiro.Interfaces.Services;
using Core.V1.Financeiro.DocumentoFinanceiro.Models;

namespace Core.V1.Financeiro.DocumentoFinanceiro.Services
{
    public class DocumentoFinanceiroService : IDocumentoFinanceiroService
    {
        private readonly IDocumentoFinanceiroRepository _documentoFinanceiroRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public DocumentoFinanceiroService(IDocumentoFinanceiroRepository documentoFinanceiro,
        ICategoriaRepository categoriaRepository)
        {
            _documentoFinanceiroRepository = documentoFinanceiro;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<int> AddAsync(DocumentoFinanceiroModel documentoFinanceiro)
        {            
            return await _documentoFinanceiroRepository.AddAsync(documentoFinanceiro);
        }
        
        public async Task<int> UpdateAsync(int id, DocumentoFinanceiroModel documentoFinanceiro)
        {
            return await _documentoFinanceiroRepository.UpdateAsync(id, documentoFinanceiro);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _documentoFinanceiroRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DocumentoFinanceiroModel>> GetAllAsync(int usuarioId, string tipoReceitaDespesa)
        {
            return await _documentoFinanceiroRepository.GetAllAsync(usuarioId, tipoReceitaDespesa);
        }

        public async Task<DocumentoFinanceiroModel> GetByIdAsync(int id, int usuarioId)
        {
            return await _documentoFinanceiroRepository.GetByIdAsync(id, usuarioId);
        }

        public Task<int> PagamentoAsync(int id, DocumentoFinanceiroModel documentoFinanceiro)
        {
            return _documentoFinanceiroRepository.PagamentoAsync(id, documentoFinanceiro);
        }
    }
}