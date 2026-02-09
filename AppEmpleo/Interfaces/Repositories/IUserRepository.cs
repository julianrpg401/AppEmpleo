using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IUserRepository : IAddAsync<UserAccount>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<UserAccount?> GetByEmailAsync(string email);
        Task<Candidate?> GetCandidateByUserIdAsync(int userId);
    }
}
