using Microsoft.AspNetCore.Mvc;
using Core.V1.Login.Interfaces.Services;
using Core.V1.Login.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService authService)
        {
            _loginService = authService;
        }

        /// <summary>
        /// Método responsável pelo login
        /// </summary>
        /// <param name="usuario">Instância da classe LoginModel</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel usuario)
        {
            LoginResponse response = await _loginService.Autenticacao(usuario.Usuario, usuario.Senha);

            if (response.Token == null || string.IsNullOrWhiteSpace(response.Token))
                return Unauthorized();

            return Ok(new {response.Token, response.NomeUsuario});
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            // Retorna uma resposta 200 OK com uma mensagem simples
            return Ok(new { message = "API está funcionando" });
        }
    }
}
