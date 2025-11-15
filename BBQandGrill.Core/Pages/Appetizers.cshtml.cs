using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BBQandGrill.Core.Pages
{
    public class AppetizersModel : PageModel
    {
        private readonly ILogger<AppetizersModel> _logger;

        public AppetizersModel(ILogger<AppetizersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Appetizers page accessed");
        }
    }
}
