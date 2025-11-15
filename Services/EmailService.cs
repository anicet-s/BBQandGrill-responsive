using System;
using System.Net;
using System.Net.Mail;
using BBQandGrill.Helpers;

namespace BBQandGrill.Services
{
    /// <summary>
    /// Service for sending emails with proper error handling and logging
    /// </summary>
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly bool _enableSsl;

        public EmailService()
        {
            _smtpHost = ConfigurationHelper.GetSmtpHost();
            _smtpPort = ConfigurationHelper.GetSmtpPort();
            _smtpUsername = ConfigurationHelper.GetSmtpUsername();
            _smtpPassword = ConfigurationHelper.GetSmtpPassword();
            _fromEmail = ConfigurationHelper.GetSmtpFromEmail();
            _enableSsl = ConfigurationHelper.GetSmtpEnableSsl();
        }

        /// <summary>
        /// Sends a contact form email
        /// </summary>
        public EmailResult SendContactEmail(string senderName, string senderEmail, string message)
        {
            if (string.IsNullOrWhiteSpace(senderName))
                return EmailResult.Failure("Sender name is required");
            
            if (string.IsNullOrWhiteSpace(senderEmail))
                return EmailResult.Failure("Sender email is required");
            
            if (string.IsNullOrWhiteSpace(message))
                return EmailResult.Failure("Message is required");

            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_fromEmail);
                    mailMessage.To.Add(new MailAddress(_fromEmail)); // Send to restaurant email
                    mailMessage.ReplyToList.Add(new MailAddress(senderEmail)); // Allow reply to sender
                    mailMessage.Subject = $"Contact Form Message from {senderName}";
                    mailMessage.Body = $"From: {senderName}\nEmail: {senderEmail}\n\nMessage:\n{message}";
                    mailMessage.IsBodyHtml = false;

                    using (SmtpClient client = CreateSmtpClient())
                    {
                        client.Send(mailMessage);
                    }
                }

                return EmailResult.Success("Message sent successfully! We'll get back to you soon.");
            }
            catch (SmtpException ex)
            {
                // Log the exception (implement logging in Phase 4)
                return EmailResult.Failure(
                    "Sorry, we couldn't send your message at this time. Please try again later or contact us directly.");
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging in Phase 4)
                return EmailResult.Failure("An unexpected error occurred. Please try again later.");
            }
        }

        private SmtpClient CreateSmtpClient()
        {
            SmtpClient client = new SmtpClient
            {
                Host = _smtpHost,
                Port = _smtpPort,
                EnableSsl = _enableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword)
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
