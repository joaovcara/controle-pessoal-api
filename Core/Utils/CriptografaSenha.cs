using System.Security.Cryptography;
using System.Text;

namespace Core.Utils
{
    public class CriptografaSenha
    {
        /// <summary>
        /// Método responsável por gerar a senha criptografada
        /// </summary>
        /// <param name="password">Senha digitada pelo usuário</param>
        /// <returns>Senha criptografada</returns>
        public static Dictionary<string, string> GeneratedPasswordCripto(string password)
        {
            string salt = GenerateSalt();
            string passwordCripto = HashPassword(password, salt);

            return new Dictionary<string, string>
            {
                {"PasswordCripto", passwordCripto},
                {"Salt", salt}
            };
        }

        /// <summary>
        /// Gera o salt
        /// </summary>
        /// <returns>String com salt base64</returns>
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[64];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Criptografa a senha com SHA-256 e salt
        /// </summary>
        /// <param name="password">Senha do usuário</param>
        /// <param name="salt">Chave salta gerada</param>
        /// <returns>Hash da senha do usuário</returns>
        private static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Combina a senha e o salt
                var combinedPasswordSalt = string.Concat(password, salt);
                // Converte para bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(combinedPasswordSalt);
                // Aplica o hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                // Converte o hash para string
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Método para verificar se uma senha corresponde ao hash armazenado
        /// </summary>
        /// <param name="enteredPassword">Senha digitada pelo usuário</param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            // Criptografa a senha digitada usando o mesmo salt
            var enteredHash = HashPassword(enteredPassword, storedSalt);
            // Compara o hash gerado com o hash armazenado
            return enteredHash == storedHash;
        }
    }

}