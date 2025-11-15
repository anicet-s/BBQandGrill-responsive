using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BBQandGrill.Core.Pages
{
    public class AboutModel : PageModel
    {
        private readonly ILogger<AboutModel> _logger;

        public AboutModel(ILogger<AboutModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("About page accessed");
        }
    }
}
