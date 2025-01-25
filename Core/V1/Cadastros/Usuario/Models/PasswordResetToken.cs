namespace Core.V1.Cadastros.Usuario.Models
{
    public class PasswordResetToken
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}