using Core.V1.Cadastros.Categoria.Models;
using System.Threading.Tasks;

namespace Core.V1.Cadastros.Categoria.Interfaces.Services
{
    public interface ICategoriaService
    {
        Task<int> AddAsync(CategoriaModel categoria);
        Task<int> UpdateAsync(int id, CategoriaModel categoria);
        Task<int> DeleteAsync(int id, int usuarioId);
        Task<CategoriaModel> GetByIdAsync(int id, int usuarioId);
        Task<IEnumerable<CategoriaModel>> GetAllAsync(int usuarioId);
        Task<IEnumerable<CategoriaModel>> GetAllByTipoReceitaDespesaAsync(string tipoReceitaDespesa, int usuarioId);
    }
}