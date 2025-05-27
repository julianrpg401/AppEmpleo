using AppEmpleo.Class.Services.SessionServices;
using AppEmpleo.Interfaces.Services.SessionServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IClaimsService? _claimsService;

        public IndexModel(IClaimsService claimsService)
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
