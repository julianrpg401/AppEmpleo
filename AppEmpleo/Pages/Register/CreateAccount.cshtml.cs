using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Utilities;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.CreateAccount
{
    public class CreateAccountModel : PageModel
    {
        private readonly UserRepository _userRepository;

        [BindProperty]
        public new Usuario User { get; set; } = null!;

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

            var existingUser = await _userRepository.ValidateExistingUserAsync(User);

            // Valida si el correo electrónico ya existe
            if (existingUser != null)
            {
                Console.WriteLine("El correo electrónico ya está registrado");
                return Page();
            }

            User = UserDataProcessor.UserFormat(User);
            await _userRepository.AddUserAsync(User);

            return RedirectToPage("/Register/RegisterSuccess");
        }
    }
}
