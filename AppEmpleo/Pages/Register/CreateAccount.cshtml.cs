using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Utilities;
using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.CreateAccount
{
    public class CreateAccountModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        [BindProperty]
        public new Usuario User { get; set; } = null!;

        public CreateAccountModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void OnGet()
        {
        }

        // A�ade un usuario a la base de datos
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Estado del modelo no valido");
                return Page();
            }

            var existingUser = await _userRepository.ValidateExistingUserAsync(User);

            // Valida si el correo electr�nico ya existe
            if (existingUser != null)
            {
                Console.WriteLine("El correo electr�nico ya est� registrado");
                return Page();
            }

            User = UserDataProcessor.UserFormat(User);
            await _userRepository.AddAsync(User);

            return RedirectToPage("/Register/RegisterSuccess");
        }
    }
}
