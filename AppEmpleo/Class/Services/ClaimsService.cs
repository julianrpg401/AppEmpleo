using AppEmpleo.Models;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace AppEmpleo.Class.Services
{
    public class ClaimsService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ClaimsService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

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
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException();
        }

        public bool AuthenticatedUser()
            => _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

        public string GetClaim(string claimType)
            => _contextAccessor.HttpContext?.User.FindFirst(claimType)?.Value ?? string.Empty;

        public int GetId()
        {
            var userIdClaim = GetClaim("UserId");

            return int.TryParse(userIdClaim, out int userId) ? userId : throw new InvalidOperationException("El UserId no es válido.");
        }

        public string GetName()
            => GetClaim(ClaimTypes.Name);

        public string GetEmail()
            => GetClaim(ClaimTypes.Email);

        public string GetRole()
            => GetClaim(ClaimTypes.Role);
    }
}
