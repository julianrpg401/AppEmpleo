using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class UserRepository : Repository<Usuario>, IValidateUserAsync
    {
        // Inyectar la base de datos
        public UserRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext)
        {
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

        // Devuelve un usuario con el rol y su respectivo id
        public async Task<Usuario> GetUserAsync(Usuario user)
        {
            try
            {
                Usuario? foundUser;

                switch (user.Rol)
                {
                    case "RECLUTADOR":

                        foundUser = await _appEmpleoContext.Usuarios
                            .Include(r => r.Reclutador)
                            .FirstOrDefaultAsync(r => r.UsuarioId == user.UsuarioId);

                        if (foundUser == null)
                        {
                            throw new ArgumentException("No se encontró el reclutador");
                        }

                        return foundUser;

                    case "CANDIDATO":

                        foundUser = await _appEmpleoContext.Usuarios
                            .Include(c => c.Candidato)
                            .FirstOrDefaultAsync(c => c.UsuarioId == user.UsuarioId);

                        if (foundUser == null)
                        {
                            throw new ArgumentException("No se encontró el reclutador");
                        }

                        return foundUser;

                    default:
                        throw new ArgumentException("Rol no válido");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException("Error al validar el usuario");
        }
    }
}
