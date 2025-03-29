using AppEmpleo.Class.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ClaimsService? _claimsService;

        public IndexModel(ClaimsService claimsService)
        {
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
