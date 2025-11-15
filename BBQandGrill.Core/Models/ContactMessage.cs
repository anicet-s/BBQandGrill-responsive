using System;
using System.ComponentModel.DataAnnotations;

namespace BBQandGrill.Core.Models
{
    /// <summary>
    /// Represents a contact form message
    /// </summary>
    public class ContactMessage
    {
        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string SenderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string SenderEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a message")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 1000 characters")]
        public string Message { get; set; } = string.Empty;

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
