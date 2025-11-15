using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BBQandGrill.Core.Pages
{
    public class DessertsModel : PageModel
    {
        private readonly ILogger<DessertsModel> _logger;

        public DessertsModel(ILogger<DessertsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Desserts page accessed");
        }
    }
}
