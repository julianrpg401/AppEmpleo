using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly AppEmpleoContext _appEmpleoContext;
        private Usuario usuario;

        public Usuario Usuario { get => usuario; set => usuario = value; }

        public RegisterModel(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(Usuario usuario)
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
                ModelState.AddModelError("usuario.Email", "El correo electrónico ya está registrado.");
                Console.WriteLine("usuario.Email", "El correo electrónico ya está registrado.");
                return Page();
            }

            await _appEmpleoContext.Usuarios.AddAsync(usuario);
            await _appEmpleoContext.SaveChangesAsync();

            return RedirectToPage("/Register/RegisterSuccess");
        }
    }
}
