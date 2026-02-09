using AppEmpleo.Class.Services.SessionServices;
using AppEmpleo.Interfaces.Services.SessionServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IClaimsService _claimsService;

        public IndexModel(IClaimsService claimsService)
        {
            _claimsService = claimsService;
        }

        public async Task OnGetAsync()
        {
            if (_claimsService.AuthenticatedUser())
            {
                await _claimsService.UserLogout();
            }
        }
    }
}
