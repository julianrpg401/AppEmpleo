using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Services
{
    public interface IUserService
    {
        Task<Usuario?> RegisterUser(Usuario user);
        Task<bool> Login(string email, string password);
        Usuario GetUserClaims();
        Task<Candidato?> GetCandidateAsync(int userId);
    }
}
