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
        private readonly AppEmpleoContext _appEmpleoContext;
        private UserRepository _userRepository;

        [BindProperty]
        public Usuario Usuario { get; set; } = null!;

        public CreateAccountModel(AppEmpleoContext appEmpleoContext)
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
