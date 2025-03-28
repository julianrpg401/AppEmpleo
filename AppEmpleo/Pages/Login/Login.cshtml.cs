using AppEmpleo.Class.Services;
using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        [Required(ErrorMessage = "El campo Email no puede estar vac�o")]
        [EmailAddress(ErrorMessage = "El correo electr�nico no tiene un formato v�lido")]
        public string Email { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "El campo Contrase�a no puede estar vac�o")]
        [DisplayName("Contrase�a")]
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
                Console.WriteLine("Estado del modelo no v�lido");
                return Page();
            }

            var existingUser = await _userRepository.ValidateExistingUserAsync(Email);

            if (existingUser == null)
            {
                Console.WriteLine("El correo electr�nico no est� registrado");
                return Page();
            }

            Password = EncryptService.GetSHA256(Password);

            if (existingUser.ClaveHash != Password)
            {
                Console.WriteLine("Contrase�a incorrecta");
                return Page();
            }

            if (existingUser == null | existingUser?.ClaveHash != Password)
            {
                Console.WriteLine("Error al autenticar al usuario");
                return Page();
            }

            await _claimsService.UserLogin(existingUser!);

            return RedirectToPage("/Application/Home");
        }
    }
}
