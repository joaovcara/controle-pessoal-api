using System.ComponentModel.DataAnnotations;

namespace Core.V1.Cadastros.Usuario.Models
{
    public class UsuarioRequest
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Usuario { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string Email { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}