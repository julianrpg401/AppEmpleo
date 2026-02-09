using Microsoft.AspNetCore.Http;

namespace AppEmpleo.Interfaces.Services.Storage
{
    public interface IResumeStorage
    {
        Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken = default);
        string GetAbsolutePath(string storedFileName);
        bool Exists(string storedFileName);
    }
}
