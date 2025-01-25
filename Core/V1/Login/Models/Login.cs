namespace Core.V1.Login.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Obtém ou define o usuário
        /// </summary>
        public string Usuario { get; set; } = string.Empty;
        
        /// <summary>
        /// Obtém ou define a senha
        /// </summary>
        public string Senha { get; set; } = string.Empty;
    }
}