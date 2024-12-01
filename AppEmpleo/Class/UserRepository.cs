using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AppEmpleo.Class
{
    public class UserRepository
    {
        private readonly AppEmpleoContext _appEmpleoContext;

        public UserRepository(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        // Validar si el correo electrónico ya está registrado
        public async Task<Usuario?> ValidateExistingUserAsync(Usuario usuario)
        {
            try
            {
                var existingUser = await _appEmpleoContext.Usuarios.FirstOrDefaultAsync
                    (u => u.Email == usuario.Email);

                return existingUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException("Error al validar el email");
        }

        // Validar si el correo electrónico ya está registrado (sobrecarga)
        public async Task<Usuario?> ValidateExistingUserAsync(string email)
        {
            try
            {
                var existingUser = await _appEmpleoContext.Usuarios.FirstOrDefaultAsync
                    (u => u.Email == email);
                
                return existingUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException("Error al validar el email");
        }

        // Agregar el usuario a la BD
        public async Task AddUserAsync(Usuario usuario)
        {
            await _appEmpleoContext.Usuarios.AddAsync(usuario);
            await _appEmpleoContext.SaveChangesAsync();
        }
    }
}
