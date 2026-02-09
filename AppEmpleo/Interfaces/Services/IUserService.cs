using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserAccount?> RegisterUserAsync(UserAccount user);
        Task<bool> LoginAsync(string email, string password);
        UserAccount GetCurrentUserFromClaims();
        Task<Candidate?> GetCandidateByUserIdAsync(int userId);
    }
}
