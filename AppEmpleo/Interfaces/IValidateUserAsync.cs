using AppEmpleo.Models;

namespace AppEmpleo.Interfaces
{
    public interface IValidateUserAsync
    {
        Task<Usuario?> ValidateExistingUserAsync(Usuario user);
        Task<Usuario?> ValidateExistingUserAsync(string email);
    }
}
