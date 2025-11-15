using System;
using BBQandGrill.Services;

namespace BBQandGrill
{
    public partial class ContactUs : System.Web.UI.Page
    {
        private readonly EmailService _emailService;

        public ContactUs()
        {
            _emailService = new EmailService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page initialization
        }

        protected void SendMessage(object sender, EventArgs e)
        {
            string senderName = YourName.Text?.Trim();
            string senderEmail = YourEmail.Text?.Trim();
            string message = Comments.Text?.Trim();

            EmailResult result = _emailService.SendContactEmail(senderName, senderEmail, message);

            errorMessage.Text = result.Message;

            if (result.IsSuccess)
            {
                // Clear form on success
                YourName.Text = string.Empty;
                YourEmail.Text = string.Empty;
                Comments.Text = string.Empty;
            }
        }
    }
}