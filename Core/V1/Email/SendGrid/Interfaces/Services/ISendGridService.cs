namespace Core.V1.Email.SendGrid.Interfaces.Services
{
    public interface ISendGridService
    {
        Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent);
    }
}
