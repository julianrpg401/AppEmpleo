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
        private Usuario usuario;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        [DisplayName("Contrase�a")]
        public string Clave { get; set; }

        public LoginModel(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Estado del modelo no v�lido");
                Console.WriteLine(Email);
                Console.WriteLine(Clave);

                return Page();
            }

            Console.WriteLine(Clave);

            string claveHash = Encrypt.GetSHA256(Clave);

            var existingUser = await _appEmpleoContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == Email && u.ClaveHash == claveHash);

            if (existingUser == null)
            {
                ModelState.AddModelError("Usuario.Email", "El correo electr�nico no existe o no est� registrado.");
                return Page();
            }

            return RedirectToPage("/Aplication/Home");
        }
    }
}
