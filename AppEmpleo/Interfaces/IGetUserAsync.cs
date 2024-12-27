using AppEmpleo.Models;

namespace AppEmpleo.Interfaces
{
    public interface IGetUserAsync
    {
        Task<Usuario> GetUserAsync(Usuario user);
    }
}
