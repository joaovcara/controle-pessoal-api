namespace Core.V1.Email.SendGrid.Models
{
    public class SendGridModel
    {
        public string ToEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string PlainTextContent { get; set; } = string.Empty;
        public string HtmlContent { get; set; } = string.Empty;
    }
}
