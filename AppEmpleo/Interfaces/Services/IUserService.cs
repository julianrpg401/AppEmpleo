using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserAccount?> RegisterUser(UserAccount user);
        Task<bool> Login(string email, string password);
        UserAccount GetUserClaims();
        Task<Candidate?> GetCandidateAsync(int userId);
    }
}
