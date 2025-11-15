using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BBQandGrill.Core.Pages
{
    public class MenuModel : PageModel
    {
        private readonly ILogger<MenuModel> _logger;

        public MenuModel(ILogger<MenuModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Menu page accessed");
        }
    }
}
