using AppEmpleo.Class.Cryptography;
using AppEmpleo.Class.Utilities.DataProcessors;
using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Interfaces.Services;
using AppEmpleo.Interfaces.Services.SessionServices;
using AppEmpleo.Models;
using Serilog;

namespace AppEmpleo.Class.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClaimsService _claimsService;

        public UserService(IUserRepository userRepository, IClaimsService claimsService)
        {
            _userRepository = userRepository;
            _claimsService = claimsService;
        }

        // Registra un nuevo usuario
        public async Task<Usuario?> RegisterUser(Usuario user)
        {
            try
            {
                var existingUser = await _userRepository.ValidateExistingUserAsync(user);

                if (existingUser == true)
                {
                    return null;
                }

                user = UserDataProcessor.UserFormat(user);
                await _userRepository.AddAsync(user);

                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al registrar el usuario {Email}", user.Email);
                throw;
            }
        }

        // Inicia sesión con el usuario
        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var existingUser = await _userRepository.ValidateExistingUserAsync(email);

                if (existingUser == null || !EncryptService.VerifyPassword(password, existingUser.ClaveHash))
                {
                    Log.Error("El usuario {Email} no existe o la contraseña es incorrecta", email);
                    return false;
                }

                await _claimsService.UserLogin(existingUser);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al autenticar el usuario {Email}", email);
                throw;
            }
        }

        // Obtiene el usuario actual desde los claims
        public Usuario GetUserClaims()
        {
            try
            {
                var user = new Usuario()
                {
                    UsuarioId = _claimsService.GetId(),
                    Nombre = _claimsService.GetName(),
                    Email = _claimsService.GetEmail(),
                    Rol = _claimsService.GetRole()
                };

                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el usuario con ID {Id}", _claimsService.GetId());
                throw;
            }
        }

        public async Task<Candidato?> GetCandidateAsync(int userId)
        {
            try
            {
                return await _userRepository.GetCandidateByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al obtener el candidato con id: {userId}");
                throw;
            }
        }
    }
}
