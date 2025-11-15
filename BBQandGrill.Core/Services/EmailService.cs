using System.Net;
using System.Net.Mail;
using BBQandGrill.Core.Helpers;
using BBQandGrill.Core.Models;

namespace BBQandGrill.Core.Services
{
    public interface IEmailService
    {
        Task<EmailResult> SendContactEmailAsync(ContactMessage message);
    }

    /// <summary>
    /// Service for sending emails with proper error handling and logging
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly ConfigurationHelper _configHelper;
        private readonly ILogger<EmailService> _logger;

        public EmailService(ConfigurationHelper configHelper, ILogger<EmailService> logger)
        {
            _configHelper = configHelper;
            _logger = logger;
        }

        /// <summary>
        /// Sends a contact form email asynchronously
        /// </summary>
        public async Task<EmailResult> SendContactEmailAsync(ContactMessage message)
        {
            if (!message.IsValid())
            {
                return EmailResult.Failure("Please provide valid name, email, and message");
            }

            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    string fromEmail = _configHelper.GetSmtpFromEmail();
                    
                    mailMessage.From = new MailAddress(fromEmail);
                    mailMessage.To.Add(new MailAddress(fromEmail)); // Send to restaurant email
                    mailMessage.ReplyToList.Add(new MailAddress(message.SenderEmail)); // Allow reply to sender
                    mailMessage.Subject = $"Contact Form Message from {message.SenderName}";
                    mailMessage.Body = $"From: {message.SenderName}\nEmail: {message.SenderEmail}\n\nMessage:\n{message.Message}";
                    mailMessage.IsBodyHtml = false;

                    using (SmtpClient client = CreateSmtpClient())
                    {
                        await client.SendMailAsync(mailMessage);
                    }
                }

                _logger.LogInformation("Contact email sent successfully from {Email}", message.SenderEmail);
                return EmailResult.Success("Message sent successfully! We'll get back to you soon.");
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "SMTP error sending contact email from {Email}", message.SenderEmail);
                return EmailResult.Failure(
                    "Sorry, we couldn't send your message at this time. Please try again later or contact us directly.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error sending contact email from {Email}", message.SenderEmail);
                return EmailResult.Failure("An unexpected error occurred. Please try again later.");
            }
        }

        private SmtpClient CreateSmtpClient()
        {
            SmtpClient client = new SmtpClient
            {
                Host = _configHelper.GetSmtpHost(),
                Port = _configHelper.GetSmtpPort(),
                EnableSsl = _configHelper.GetSmtpEnableSsl(),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    _configHelper.GetSmtpUsername(),
                    _configHelper.GetSmtpPassword())
            };

            return client;
        }
    }

    /// <summary>
    /// Result object for email operations
    /// </summary>
    public class EmailResult
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        private EmailResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static EmailResult Success(string message) => new EmailResult(true, message);
        public static EmailResult Failure(string message) => new EmailResult(false, message);
    }
}
