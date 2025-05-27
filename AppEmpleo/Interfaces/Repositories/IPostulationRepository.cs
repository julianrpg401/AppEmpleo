using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IPostulationRepository
    {
        Task<List<Postulacion>> GetAllPostulationsAsync();
        Task<List<Postulacion>> GetPostulationsByRecruiterIdAsync(int recruiterId);
        Task<Curriculum?> GetCurriculumByIdAsync(int curriculumId);
        Task AddCurriculumAsync(Curriculum curriculum);
        Task AddPostulationAsync(Postulacion postulation);
    }
}
