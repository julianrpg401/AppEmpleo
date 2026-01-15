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
        public UserAccount NewUser { get; set; } = null!;

        public CreateAccountModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet() { }

        // A�ade un usuario a la base de datos
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var user = await _userService.RegisterUser(NewUser);

                if (user == null)
                {
                    Log.Error("El correo electr�nico {Email} ya est� registrado", NewUser.Email);
                    ModelState.AddModelError(string.Empty, "El correo electr�nico ya est� registrado. Por favor, use otro correo electr�nico.");

                    return Page();
                }

                return RedirectToPage("/Register/RegisterSuccess");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear la cuenta");
                ModelState.AddModelError(string.Empty, "Ocurri� un error al crear la cuenta. Por favor, int�ntelo de nuevo m�s tarde.");

                return Page();
            }
        }
    }
}
