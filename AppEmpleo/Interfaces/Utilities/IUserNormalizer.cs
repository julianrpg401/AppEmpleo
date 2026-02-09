using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Utilities
{
    public interface IUserNormalizer
    {
        UserAccount NormalizeForRegistration(UserAccount input);
        string NormalizeEmail(string email);
    }
}
