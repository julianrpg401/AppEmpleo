using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IUserRepository : IAddAsync<Usuario>
    {
        Task<Usuario> GetUserAsync(Usuario user);
        Task<bool> ValidateExistingUserAsync(Usuario user);
        Task<Usuario?> ValidateExistingUserAsync(string email);
        Task<Candidato?> GetCandidateByUserIdAsync(int userId);
        Task<Reclutador?> GetRecruiterByUserIdAsync(int userId);
    }
}
