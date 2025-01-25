using Core.V1.Cadastros.Usuario.Models;
using Core.V1.Cadastros.Usuario.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core.V1.Email.SendGrid.Interfaces.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISendGridService _emailService;

        public UsuarioController(IUsuarioService usuarioService, ISendGridService emailService)
        {
            _usuarioService = usuarioService;
            _emailService = emailService;
        }

        /// <summary>
        /// Adiciona um registro de usuário
        /// </summary>
        /// <param name="usuarioRequest">Instância da classe UsuarioRequest</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create(UsuarioRequest usuarioRequest)
        {
            await _usuarioService.AddAsync(usuarioRequest);
            return Ok(new
            {
                message = "Usuário cadastrado com sucesso!",
                senha = "Senha atual " + usuarioRequest.Usuario
            });
        }

        /// <summary>
        /// Altera um registro de usuário
        /// </summary>
        /// <param name="usuarioRequest">Propriedades do usuário a ser alterado</param>
        /// <returns>Sucesso ou erro</returns>
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UsuarioRequest usuarioRequest)
        {
            if (usuarioRequest.Id == 0)
                return BadRequest("Informe um Id de usuário válido.");

            await _usuarioService.UpdateAsync(usuarioRequest.Id, usuarioRequest);
            return Ok(new { message = "Usuário alterado com sucesso!" });
        }

        /// <summary>
        /// Deleta um registro de usuário
        /// </summary>
        /// <param name="id">Id do usuário a ser apagado</param>
        /// <returns>Sucesso ou erro</returns>
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usuarioService.DeleteAsync(id);
            return Ok(new { message = "Usuário deletado com sucesso!" });
        }

        /// <summary>
        /// Consulta todos os registros de usuário
        /// </summary>
        /// <returns>Sucesso ou erro</returns>
        [Authorize]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Consulta um usuário pelo Id
        /// </summary>
        /// <param name="id">Id do usuário a ser consultado</param>
        /// <returns>Sucesso ou erro</returns>
        [Authorize]
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Solicita reset de senha enviando um token temporário ao email do usuário.
        /// </summary>
        /// <param name="requestData">Email e dominio da aplicação front end</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpPost("requestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] Dictionary<string, string> requestData)
        {
            if (!requestData.TryGetValue("email", out var email) || string.IsNullOrEmpty(email))
                return BadRequest(new { message = "Email não fornecido." });

            if (!requestData.TryGetValue("frontendDomain", out var frontendDomain) || string.IsNullOrEmpty(frontendDomain))
                return BadRequest(new { message = "Domínio do front-end não fornecido." });

            if (!requestData.TryGetValue("htmlContent", out var htmlContent) || string.IsNullOrEmpty(htmlContent))
                return BadRequest(new { message = "Conteúdo HTML não fornecido." });

            if (!requestData.TryGetValue("plainTextContent", out var plainTextContent) || string.IsNullOrEmpty(plainTextContent))
                return BadRequest(new { message = "Conteúdo de texto plano não fornecido." });

            var token = await _usuarioService.GeneratePasswordResetTokenAsync(email);
            if (string.IsNullOrEmpty(token))
                return NotFound(new { message = "Usuário não encontrado." });

            // Substituir placeholders no conteúdo recebido do front-end
            htmlContent = htmlContent.Replace("{resetPasswordUrl}", $"{frontendDomain}/#/reset-senha?token={token}&email={email}");
            plainTextContent = plainTextContent.Replace("{resetPasswordUrl}", $"{frontendDomain}/#/reset-senha?token={token}&email={email}");

            var subject = "Redefinição de sua senha de acesso";
            await _emailService.SendEmailAsync(email, subject, plainTextContent, htmlContent);

            return Ok(new { message = "Link de reset de senha enviado para o email." });
        }

        /// <summary>
        /// Reseta a senha do usuário usando o token enviado.
        /// </summary>
        /// <param name="resetRequest">Dados para reset de senha</param>
        /// <returns>Sucesso ou erro</returns>
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetRequest)
        {
            if (resetRequest.NewPassword != resetRequest.ConfirmPassword)
                return BadRequest(new { message = "As senhas não coincidem." });

            var result = await _usuarioService.ResetPasswordAsync(resetRequest.Email, resetRequest.Token, resetRequest.NewPassword);
            if (!result)
                return BadRequest(new { message = "Token inválido ou expirado." });

            return Ok(new { message = "Senha resetada com sucesso!" });
        }
    }
}
