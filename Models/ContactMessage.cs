using System;

namespace BBQandGrill.Models
{
    /// <summary>
    /// Represents a contact form message
    /// </summary>
    public class ContactMessage
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; }

        public ContactMessage()
        {
            SubmittedAt = DateTime.Now;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(SenderName) &&
                   !string.IsNullOrWhiteSpace(SenderEmail) &&
                   !string.IsNullOrWhiteSpace(Message) &&
                   IsValidEmail(SenderEmail);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
