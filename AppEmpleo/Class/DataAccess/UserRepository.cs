using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using System.Threading.Tasks.Dataflow;

namespace AppEmpleo.Class.DataAccess
{
    public class UserRepository
    {
        private readonly AppEmpleoContext _appEmpleoContext;

        // Inyectar la base de datos
        public UserRepository(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        // Validar si el correo electrónico ya está registrado
        public async Task<Usuario?> ValidateExistingUserAsync(Usuario user)
        {
            try
            {
                var existingUser = await _appEmpleoContext.Usuarios.FirstOrDefaultAsync
                    (u => u.Email == user.Email);

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
        public async Task AddUserAsync(Usuario user)
        {
            await _appEmpleoContext.Usuarios.AddAsync(user);
            await _appEmpleoContext.SaveChangesAsync();
        }

        // Devuelve un usuario con el rol de reclutador y su respectivo id
        public async Task<Usuario> GetRecruiterAsync(int userId)
        {
            try
            {
                var user = await _appEmpleoContext.Usuarios
                    .Include(u => u.Reclutador)
                    .FirstOrDefaultAsync(u => u.UsuarioId == userId);

                Console.WriteLine(user?.Nombre);
                Console.WriteLine(user?.Reclutador?.ReclutadorId);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException("Error al validar el usuario");
        }
    }
}
