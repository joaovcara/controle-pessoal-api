using Core.V1.Cadastros.Categoria.Interfaces.Repositories;
using Core.V1.Cadastros.Categoria.Interfaces.Services;
using Core.V1.Cadastros.Categoria.Models;

namespace Core.V1.Cadastros.Categoria.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<int> AddAsync(CategoriaModel categoria)
        {
            return await _categoriaRepository.AddAsync(categoria);
        }

        public async Task<int> UpdateAsync(int id, CategoriaModel categoria)
        {
            return await _categoriaRepository.UpdateAsync(id, categoria);
        }

        public async Task<int> DeleteAsync(int id, int usuarioId)
        {
            return await _categoriaRepository.DeleteAsync(id, usuarioId);
        }

        public async Task<CategoriaModel> GetByIdAsync(int id, int usuarioId)
        {
            return await _categoriaRepository.GetByIdAsync(id, usuarioId);
        }

        public async Task<IEnumerable<CategoriaModel>> GetAllAsync(int usuarioId)
        {
            return await _categoriaRepository.GetAllAsync(usuarioId);
        }

        public async Task<IEnumerable<CategoriaModel>> GetAllByTipoReceitaDespesaAsync(string tipoReceitaDespesa, int usuarioId)
        {
            return await _categoriaRepository.GetAllByTipoReceitaDespesaAsync(tipoReceitaDespesa, usuarioId);
        }
    }
}