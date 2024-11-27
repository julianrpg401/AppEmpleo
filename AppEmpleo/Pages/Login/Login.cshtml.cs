using AppEmpleo.Class;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace AppEmpleo.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly AppEmpleoContext _appEmpleoContext;
        private UserRepository _userRepository;

        [BindProperty]
        public string Email { get; set; } = null!;

        [BindProperty]
        [DisplayName("Contraseña")]
        public string Clave { get; set; } = null!;

        public LoginModel(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
            _userRepository = new UserRepository(_appEmpleoContext);
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

            Clave = Encrypt.GetSHA256(Clave);

            var existingUser = await _userRepository.ValidateExistingUserAsync(Email, Clave);

            if (existingUser == null)
            {
                Console.WriteLine("El correo electrónico no está registrado");
                return Page();
            }

            return RedirectToPage("/Aplication/Home");
        }
    }
}
