using AppEmpleo.Class;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Claims;
using System.Text.Json;

namespace AppEmpleo.Pages.Login
{
    public class LoginModel : PageModel
    {
        private UserRepository _userRepository;
        private readonly ClaimsService _claimsService;

        [BindProperty]
        public string Email { get; set; } = null!;

        [BindProperty]
        [DisplayName("Contraseña")]
        public string Password { get; set; } = null!;

        public LoginModel(UserRepository userRepository, ClaimsService claimsService)
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

            Password = Encrypt.GetSHA256(Password);

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
