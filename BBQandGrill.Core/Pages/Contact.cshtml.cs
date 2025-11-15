using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BBQandGrill.Core.Models;
using BBQandGrill.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace BBQandGrill.Core.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ContactModel> _logger;

        [BindProperty]
        public ContactMessage ContactMessage { get; set; } = new ContactMessage();

        public string? ResultMessage { get; set; }
        public bool IsSuccess { get; set; }

        public ContactModel(IEmailService emailService, ILogger<ContactModel> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Contact page accessed");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _logger.LogInformation("Processing contact form submission from {Email}", ContactMessage.SenderEmail);

            var result = await _emailService.SendContactEmailAsync(ContactMessage);
            
            ResultMessage = result.Message;
            IsSuccess = result.IsSuccess;

            if (result.IsSuccess)
            {
                ContactMessage = new ContactMessage(); // Clear form
                ModelState.Clear();
            }

            return Page();
        }
    }
}
