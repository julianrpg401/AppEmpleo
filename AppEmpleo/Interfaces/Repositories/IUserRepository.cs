using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IUserRepository : IAddAsync<UserAccount>
    {
        Task<UserAccount> GetUserAsync(UserAccount user);
        Task<bool> ValidateExistingUserAsync(UserAccount user);
        Task<UserAccount?> ValidateExistingUserAsync(string email);
        Task<Candidate?> GetCandidateByUserIdAsync(int userId);
        Task<Recruiter?> GetRecruiterByUserIdAsync(int userId);
    }
}
