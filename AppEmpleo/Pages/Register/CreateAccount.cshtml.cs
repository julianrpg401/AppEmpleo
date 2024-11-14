using AppEmpleo.Class;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.CreateAccount
{
    public class CreateAccountModel : PageModel
    {
        private readonly AppEmpleoContext _appEmpleoContext;

        [BindProperty]
        public Usuario Usuario { get; set; }

        public CreateAccountModel(AppEmpleoContext appEmpleoContext)
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

            usuario.Nombre = usuario.Nombre.ToUpper();
            usuario.Apellido = usuario.Apellido.ToUpper();
            usuario.Email = usuario.Email.ToLower();
            usuario.Rol = usuario.Rol.ToUpper();

            Console.WriteLine(usuario.Nombre);
            Console.WriteLine(usuario.Apellido);
            Console.WriteLine(usuario.Rol);

            var existingUser = await _appEmpleoContext.Usuarios.FirstOrDefaultAsync
                (u => u.Email == usuario.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("usuario.Email", "El correo electrónico ya está registrado.");
                Console.WriteLine("usuario.Email", "El correo electrónico ya está registrado.");
                return Page();
            }

            usuario.ClaveHash = Encrypt.GetSHA256(usuario.ClaveHash);
            usuario.FechaRegistro = DateTime.Now;

            await _appEmpleoContext.Usuarios.AddAsync(usuario);
            await _appEmpleoContext.SaveChangesAsync();

            return RedirectToPage("/Register/RegisterSuccess");
        }
    }
}
