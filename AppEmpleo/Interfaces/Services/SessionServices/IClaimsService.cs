using AppEmpleo.Models;
using System.Security.Claims;

namespace AppEmpleo.Interfaces.Services.SessionServices
{
    public interface IClaimsService
    {
        List<Claim> CreateClaims(Usuario user);
        Task UserLogin(Usuario user);
        Task UserLogout();
        bool AuthenticatedUser();
        string GetClaim(string claimType);
        int GetId();
        string GetName();
        string GetEmail();
        string GetRole();
    }
}
