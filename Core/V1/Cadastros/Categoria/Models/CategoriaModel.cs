using System.ComponentModel.DataAnnotations.Schema;

namespace Core.V1.Cadastros.Categoria.Models
{
    [Table("Categoria")]
    public class CategoriaModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdTipoReceitaDespesa { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; } = string.Empty;
        public string Icone { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
    }
}