using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly AppEmpleoContext _appEmpleoContext;
        private Usuario usuario;
        private string? clave;

        public Usuario Usuario { get => usuario; set => usuario = value; }

        [DisplayName("Contrase�a")]
        public string? Clave { get => clave; set => clave = value; }

        public RegisterModel(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(Usuario usuario, string clave)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Estado del modelo no valido");
                return Page();
            }

            var existingUser = await _appEmpleoContext.Usuarios.FirstOrDefaultAsync
                (u => u.Email == usuario.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("usuario.Email", "El correo electr�nico ya est� registrado.");
                Console.WriteLine("usuario.Email", "El correo electr�nico ya est� registrado.");
                return Page();
            }

            await _appEmpleoContext.Usuarios.AddAsync(usuario);
            await _appEmpleoContext.SaveChangesAsync();

            return RedirectToPage("/Register/RegisterSuccess");
        }
    }
}
