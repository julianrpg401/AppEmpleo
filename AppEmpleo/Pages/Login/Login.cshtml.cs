using AppEmpleo.Class.Cryptography;
using AppEmpleo.Class.Services.SessionServices;
using AppEmpleo.Interfaces;
using AppEmpleo.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        [Required(ErrorMessage = "El campo Email no puede estar vacío")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        public string Email { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "El campo Contraseña no puede estar vacío")]
        [DisplayName("Contraseña")]
        public string Password { get; set; } = null!;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        // Autentica al usuario
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (!await _userService.Login(Email, Password))
                {
                    Log.Error("Error al autenticar el usuario {Email}", Email);
                    return Page();
                }

                return RedirectToPage("/Application/Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al iniciar sesión");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al iniciar sesión. Por favor, inténtelo de nuevo más tarde.");

                return Page();
            }
        }
    }
}
