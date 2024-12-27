using AppEmpleo.Models;

namespace AppEmpleo.Interfaces
{
    public interface IUserRepository : IAddAsync<Usuario>, IValidateUserAsync, IGetUserAsync
    {
    }
}
