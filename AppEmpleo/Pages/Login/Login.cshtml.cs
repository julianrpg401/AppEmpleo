using AppEmpleo.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        [Required(ErrorMessage = "El campo Email no puede estar vac�o")]
        [EmailAddress(ErrorMessage = "El correo electr�nico no tiene un formato v�lido")]
        public string Email { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "El campo Contrase�a no puede estar vac�o")]
        [DisplayName("Contrase�a")]
        public string Password { get; set; } = null!;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        // Authenticates the user.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!await _userService.LoginAsync(Email, Password))
            {
                return Page();
            }

            return RedirectToPage("/Application/Home");
        }
    }
}
