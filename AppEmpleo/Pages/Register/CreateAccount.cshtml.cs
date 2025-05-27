using AppEmpleo.Interfaces.Services;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace AppEmpleo.Pages.CreateAccount
{
    public class CreateAccountModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public new Usuario User { get; set; } = null!;

        public CreateAccountModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet() { }

        // Añade un usuario a la base de datos
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var user = await _userService.RegisterUser(User);

                if (user == null)
                {
                    Log.Error("El correo electrónico {Email} ya está registrado", User.Email);
                    ModelState.AddModelError(string.Empty, "El correo electrónico ya está registrado. Por favor, use otro correo electrónico.");

                    return Page();
                }

                return RedirectToPage("/Register/RegisterSuccess");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear la cuenta");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear la cuenta. Por favor, inténtelo de nuevo más tarde.");

                return Page();
            }
        }
    }
}
