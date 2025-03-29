using AppEmpleo.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Serilog;

namespace AppEmpleo.Class.Services
{
    public class ClaimsService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ClaimsService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        // Crea una lista de claims para el usuario y poder autorizarlo
        public List<Claim> CreateClaims(Usuario user)
        {
            try
            {
                var claims = new List<Claim>()
                {
                    new Claim("UserId", user.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Name, user.Nombre),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Rol)
                };

                return claims;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear los claims para el usuario {UserId}", user.UsuarioId);
                throw;
            }
        }

        public async Task UserLogin(Usuario user)
        {
            try
            {
                var claims = CreateClaims(user);
                var claimsIdentity = GetClaimsIdentity(claims);
                var claimsPrincipal = GetClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                };

                await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al autenticar al usuario {UserId}", user.UsuarioId);
                throw;
            }
        }

        public async Task UserLogout()
        {
            try
            {
                await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al cerrar la sesión del usuario");
                throw;
            }
        }

        public bool AuthenticatedUser()
        {
            try
            {
                return _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al verificar si el usuario está autenticado");
                throw;
            }
        }

        // Obtiene un claim específico del usuario
        public string GetClaim(string claimType)
        {
            try
            {
                return _contextAccessor.HttpContext?.User.FindFirst(claimType)?.Value ?? string.Empty;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el claim {ClaimType}", claimType);
                throw;
            }
        }

        // Obtiene el Id del usuario
        public int GetId()
        {
            try
            {
                var userIdClaim = GetClaim("UserId");

                return int.TryParse(userIdClaim, out int userId) ? userId : throw new InvalidOperationException("El UserId no es válido.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el Id del usuario");
                throw;
            }
        }

        // Obtiene el nombre del usuario
        public string GetName()
            => GetClaim(ClaimTypes.Name);

        // Obtiene el correo electrónico del usuario
        public string GetEmail()
            => GetClaim(ClaimTypes.Email);

        // Obtiene el rol del usuario
        public string GetRole()
            => GetClaim(ClaimTypes.Role);

        private static ClaimsPrincipal GetClaimsPrincipal(ClaimsIdentity claimsIdentity)
            => new ClaimsPrincipal(claimsIdentity);

        private static ClaimsIdentity GetClaimsIdentity(List<Claim> claims)
            => new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
