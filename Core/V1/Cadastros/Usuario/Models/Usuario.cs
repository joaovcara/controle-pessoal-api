using System.ComponentModel.DataAnnotations.Schema;

namespace Core.V1.Cadastros.Usuario.Models
{
    [Table("Usuario")]
    public class UsuarioModel
    {
        /// <summary>
        /// Obtém ou define o id do usuário
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém ou define o nome do usuário
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o usuário de acesso
        /// </summary>
        public string Usuario { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o hash da senha do usuário
        /// </summary>
        public string HashSenha { get; set; } = string.Empty;

        /// <summary>
        /// Obtem ou define o email do usuário
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define a chave de criptografia de senha do usuário
        /// </summary>
        public string Salt { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define a data de cadastro do usuário
        /// </summary>
        public DateTime DataCadastro { get; set; }

        /// <summary>
        /// Obtém ou define usuário ativo
        /// </summary>
        public bool Ativo { get; set; }
    }
}
