using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Services
{
    public interface IPostulationService
    {
        Task CreatePostulation(int offerId, Candidato candidate, IFormFile CVFile);
        Task<List<Postulacion>> GetAllPostulationsAsync(int recruiterId);
    }
}
