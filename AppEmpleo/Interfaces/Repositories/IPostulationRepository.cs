using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IPostulationRepository
    {
        Task<List<JobApplication>> GetAllPostulationsAsync();
        Task<List<JobApplication>> GetPostulationsByRecruiterIdAsync(int recruiterId);
        Task<Resume?> GetCurriculumByIdAsync(int curriculumId);
        Task AddCurriculumAsync(Resume curriculum);
        Task AddPostulationAsync(JobApplication postulation);
    }
}
