using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppEmpleo.Class.DataAccess
{
    public class UserRepository : Repository<Usuario>, IUserRepository
    {
        // Pasa el contexto a la clase base
        public UserRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext) { }

        // Valida si el correo electrónico ya está registrado
        public async Task<bool> ValidateExistingUserAsync(Usuario user)
        {
            try
            {
                var existingUser = await _appEmpleoContext.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingUser != null)
                {
                    Log.Error("El correo electrónico {Email} ya está registrado", user.Email);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al validar el email para el usuario {Email}", user.Email);
                throw new ArgumentException("Error al validar el email", ex);
            }
        }

        // Valida si el correo electrónico ya está registrado (sobrecarga)
        public async Task<Usuario?> ValidateExistingUserAsync(string email)
        {
            try
            {
                var existingUser = await _appEmpleoContext.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (existingUser == null)
                {
                    Log.Error("El correo electrónico {Email} no está registrado", email);
                    return null;
                }

                return existingUser;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al validar el email {Email}", email);
                throw new ArgumentException("Error al validar el email", ex);
            }
        }

        // Obtiene un usuario con el rol y su respectivo id
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
                            Log.Error("No se encontró el reclutador {UserId}", user.UsuarioId);
                            throw new ArgumentException("No se encontró el reclutador");
                        }

                        return foundUser;

                    case "CANDIDATO":

                        foundUser = await _appEmpleoContext.Usuarios
                            .Include(c => c.Candidato)
                            .FirstOrDefaultAsync(c => c.UsuarioId == user.UsuarioId);

                        if (foundUser == null)
                        {
                            Log.Error("No se encontró el candidato {UserId}", user.UsuarioId);
                            throw new ArgumentException("No se encontró el reclutador");
                        }

                        return foundUser;

                    default:
                        Log.Error("Rol no válido {Rol}", user.Rol);
                        throw new ArgumentException("Rol no válido");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el usuario con el rol {Rol} y el id {UsuarioId}", user.Rol, user.UsuarioId);
                throw new ArgumentException("Error al validar el usuario", ex);
            }
        }

        // Obtiene un candidato por su ID de usuario
        public async Task<Candidato?> GetCandidateByUserIdAsync(int userId)
        {
            try
            {
                return await _appEmpleoContext.Candidatos
                    .FirstOrDefaultAsync(c => c.UsuarioId == userId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el candidato por usuarioId {UsuarioId}", userId);
                throw new ArgumentException("Error al obtener el candidato", ex);
            }
        }

        // Obtiene un reclutador por su ID de usuario
        public async Task<Reclutador?> GetRecruiterByUserIdAsync(int userId)
        {
            try
            {
                return await _appEmpleoContext.Reclutadores
                    .FirstOrDefaultAsync(r => r.UsuarioId == userId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el reclutador por usuarioId {UsuarioId}", userId);
                throw new ArgumentException("Error al obtener el reclutador", ex);
            }
        }
    }
}
