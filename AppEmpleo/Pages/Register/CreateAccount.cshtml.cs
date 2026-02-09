using AppEmpleo.Interfaces.Services;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.CreateAccount
{
    public class CreateAccountModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public UserAccount NewUser { get; set; } = null!;

        public CreateAccountModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet() { }

        // Creates a new user account.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userService.RegisterUserAsync(NewUser);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "El correo electrónico ya está registrado. Por favor, use otro correo electrónico.");
                return Page();
            }

            return RedirectToPage("/Register/RegisterSuccess");
        }
    }
}
