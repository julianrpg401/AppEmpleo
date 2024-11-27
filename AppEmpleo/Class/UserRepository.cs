using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

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
            var existingUser = await _appEmpleoContext.Usuarios.FirstOrDefaultAsync
                (u => u.Email == usuario.Email);

            return existingUser;
        }

        public async Task AddUserAsync(Usuario usuario)
        {
            await _appEmpleoContext.Usuarios.AddAsync(usuario);
            await _appEmpleoContext.SaveChangesAsync();
        }
    }
}
