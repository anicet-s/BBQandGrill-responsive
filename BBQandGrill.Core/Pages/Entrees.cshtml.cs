using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BBQandGrill.Core.Pages
{
    public class EntreesModel : PageModel
    {
        private readonly ILogger<EntreesModel> _logger;

        public EntreesModel(ILogger<EntreesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Entrees page accessed");
        }
    }
}
