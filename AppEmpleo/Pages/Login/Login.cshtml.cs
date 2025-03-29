using AppEmpleo.Class.Services;
using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AppEmpleo.Pages.Login
{
    public class LoginModel : PageModel
    {
        private IUserRepository _userRepository;
        private readonly ClaimsService _claimsService;

        [BindProperty]
        [Required(ErrorMessage = "El campo Email no puede estar vacío")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        public string Email { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "El campo Contraseña no puede estar vacío")]
        [DisplayName("Contraseña")]
        public string Password { get; set; } = null!;

        public LoginModel(IUserRepository userRepository, ClaimsService claimsService)
        {
            _userRepository = userRepository;
            _claimsService = claimsService;
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
                var existingUser = await _userRepository.ValidateExistingUserAsync(Email);

                if (existingUser == null)
                {
                    return Page();
                }

                Password = EncryptService.GetSHA256(Password);

                if (existingUser.ClaveHash != Password)
                {
                    return Page();
                }

                if (existingUser == null | existingUser?.ClaveHash != Password)
                {
                    return Page();
                }

                await _claimsService.UserLogin(existingUser!);

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
