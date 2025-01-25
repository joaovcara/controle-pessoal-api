using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.V1.Login.Interfaces.Services;
using Core.V1.Cadastros.Usuario.Interfaces.Repositories;
using Core.V1.Cadastros.Usuario.Models;
using Core.Utils;
using Core.V1.Login.Models;

namespace Core.V1.Login.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly string _secretKey;

        public LoginService(IConfiguration configuration,
                            IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _secretKey = configuration["Users:SecretKeyUser"] ?? string.Empty;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<LoginResponse> Autenticacao(string username, string password)
        {
            UsuarioModel usuario = await _usuarioRepository.GetUser(username);
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.NomeUsuario = usuario.Nome;

            if (username != usuario.Usuario || !CriptografaSenha.VerifyPassword(password, usuario.HashSenha, usuario.Salt) || !usuario.Ativo) 
                return new LoginResponse();

            // Criação do token JWT
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("Id", usuario.Id.ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"])),
                signingCredentials: signinCredentials
            );

            loginResponse.Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return loginResponse;
        }
    }
}
