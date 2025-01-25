using Core.V1.Email.SendGrid.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Core.V1.Email.SendGrid.Services
{
    public class SendGridService : ISendGridService
    {
        private readonly string _apiKey;

        public SendGridService(IConfiguration configuration)
        {
            // Recuperando a chave da API do SendGrid do appsettings.json
            _apiKey = configuration["SendGrid:ApiKey"] ?? "";
        }

        public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("controle-pessoal@hotmail.com", "Controle Pessoal");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);

            // Você pode adicionar um tratamento de erro ou verificar o status da resposta aqui
        }

        // Posteriormente implementar uma tabela de configuração para buscar o email e o name do banco
    }
}
