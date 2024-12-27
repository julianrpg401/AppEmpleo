using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Services;
using AppEmpleo.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Security.Claims;

namespace AppEmpleo.Pages.Login
{
    public class LoginModel : PageModel
    {
        private IUserRepository _userRepository;
        private readonly ClaimsService _claimsService;

        [BindProperty]
        public string Email { get; set; } = null!;

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Estado del modelo no válido");
                return Page();
            }

            var existingUser = await _userRepository.ValidateExistingUserAsync(Email);

            if (existingUser == null)
            {
                Console.WriteLine("El correo electrónico no está registrado");
                return Page();
            }

            Password = EncryptService.GetSHA256(Password);

            if (existingUser.ClaveHash != Password)
            {
                Console.WriteLine("Contraseña incorrecta");
                return Page();
            }

            var claims = _claimsService.CreateClaims(existingUser);

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToPage("/Application/Home");
        }
    }
}
