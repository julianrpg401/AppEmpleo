using AppEmpleo.Class.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ClaimsService? _claimsService;

        public IndexModel(ILogger<IndexModel> logger, ClaimsService claimsService)
        {
            _logger = logger;
            _claimsService = claimsService;

            if (_claimsService.AuthenticatedUser())
            {
                _claimsService?.UserLogout();
            }
        }

        public void OnGet()
        {

        }
    }
}
