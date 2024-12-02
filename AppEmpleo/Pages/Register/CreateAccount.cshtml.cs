using AppEmpleo.Class;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.CreateAccount
{
    public class CreateAccountModel : PageModel
    {
        private readonly UserRepository _userRepository;

        [BindProperty]
        public Usuario Usuario { get; set; } = null!;

        public CreateAccountModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Estado del modelo no valido");
                return Page();
            }

            var existingUser = await _userRepository.ValidateExistingUserAsync(Usuario);

            // Valida si el correo electrónico ya existe
            if (existingUser != null)
            {
                Console.WriteLine("El correo electrónico ya está registrado");
                return Page();
            }

            Usuario = Data.UserFormat(Usuario);
            await _userRepository.AddUserAsync(Usuario);

            return RedirectToPage("/Register/RegisterSuccess");
        }
    }
}
