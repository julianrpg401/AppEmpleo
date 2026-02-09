using AppEmpleo.Class.Utilities.Normalization;
using AppEmpleo.Interfaces.Services.SessionServices;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace AppEmpleo.Class.Services.SessionServices
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ClaimsService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        // Crea una lista de claims para el usuario y poder autorizarlo

        public async Task UserLogin(UserAccount user)
        {
            var claims = CreateClaims(user);
            var claimsIdentity = GetClaimsIdentity(claims);
            var claimsPrincipal = GetClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
            };

            await _contextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties);
        }

        // Cierra la sesión del usuario
        public async Task UserLogout()
        {
            await _contextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // Crea una lista de claims para el usuario
        public List<Claim> CreateClaims(UserAccount user)
        {
            var claims = new List<Claim>()
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            if (user.Role == RoleConstants.Candidate && user.Candidate != null)
            {
                claims.Add(new Claim(ClaimTypes.UserData, user.Candidate.CandidateId.ToString()));
            }
            else if (user.Role == RoleConstants.Recruiter && user.Recruiter != null)
            {
                claims.Add(new Claim(ClaimTypes.UserData, user.Recruiter.RecruiterId.ToString()));
            }

            return claims;
        }

        // Verifica si el usuario está autenticado
        public bool AuthenticatedUser()
        {
            return _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        // Obtiene un claim específico del usuario
        public string GetClaim(string claimType)
        {
            return _contextAccessor.HttpContext?.User.FindFirst(claimType)?.Value ?? string.Empty;
        }

        // Obtiene el Id del usuario
        public int GetId()
        {
            var userIdClaim = GetClaim("UserId");
            return int.TryParse(userIdClaim, out int userId)
                ? userId
                : throw new InvalidOperationException("UserId claim is invalid.");
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

        public string GetUserData()
            => GetClaim(ClaimTypes.UserData);

        // Métodos privados para crear ClaimsIdentity y ClaimsPrincipal
        private static ClaimsPrincipal GetClaimsPrincipal(ClaimsIdentity claimsIdentity)
            => new ClaimsPrincipal(claimsIdentity);

        private static ClaimsIdentity GetClaimsIdentity(List<Claim> claims)
            => new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
