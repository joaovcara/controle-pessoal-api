using Core.V1.Email.SendGrid.Interfaces.Services;
using Core.V1.Email.SendGrid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class SendGridController : ControllerBase
    {
        private readonly ISendGridService _sendGridService;

        public SendGridController(ISendGridService sendGridService)
        {
            _sendGridService = sendGridService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] SendGridModel emailRequest)
        {
            await _sendGridService.SendEmailAsync(
                emailRequest.ToEmail, 
                emailRequest.Subject, 
                emailRequest.PlainTextContent, 
                emailRequest.HtmlContent
            );
            
            return Ok("Email enviado com sucesso.");
        }
    }
}
