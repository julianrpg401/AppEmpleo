using AppEmpleo.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly AppEmpleoContext _appEmpleoContext;
        private Usuario usuario;

        [BindProperty]
        [Required]
        public Usuario Usuario { get => usuario; set => usuario = value; }

        public RegisterModel(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        public void OnGet()
        {
        }

        public string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            // Almacenar tanto el salt como el hash
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existingUser = await _appEmpleoContext.Usuarios.FirstOrDefaultAsync
                (u => u.EmailUsuario == Usuario.EmailUsuario);

            if (existingUser != null)
            {
                ModelState.AddModelError("User.Email", "El correo electrónico ya está registrado.");

                return Page();
            }

            Usuario.ClaveUsuario = HashPassword(Usuario.ClaveUsuario);

            _appEmpleoContext.Usuarios.Add(Usuario);
            await _appEmpleoContext.SaveChangesAsync();

            return Page();
        }  
    }
}
