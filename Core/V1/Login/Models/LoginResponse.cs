namespace Core.V1.Login.Models
{
    public class LoginResponse
    {
        /// <summary>
        /// Obtém ou define o nome do usuário
        /// </summary>
        public string NomeUsuario { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o token
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}