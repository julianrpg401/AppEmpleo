using AppEmpleo.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly AppEmpleoContext? _appEmpleoContext;
        private Usuario? usuario;

        [BindProperty]
        public Usuario? Usuario { get => usuario; set => usuario = value; }

        public RegisterModel(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //        return Page();

        //    var existingUser = await _appEmpleoContext.Usuarios.FirstOrDefaultAsync
        //        (u => u.EmailUsuario == Usuario.EmailUsuario);

        //    if (existingUser != null)
        //    {
        //        ModelState.AddModelError("User.Email", "El correo electrónico ya está registrado.");

        //        return Page();
        //    }

        //    _appEmpleoContext.Usuarios.Add(Usuario);
        //    await _appEmpleoContext.SaveChangesAsync();

        //    return Page();
        //}  
    }
}
