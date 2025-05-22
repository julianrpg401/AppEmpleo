using AppEmpleo.Models;

namespace AppEmpleo.Interfaces
{
    public interface IApplicationRepository
    {
        Task<List<Postulacion>> GetAllPostulacionesAsync();
        Task<Curriculum?> GetCurriculumByIdAsync(int curriculumId);
        Task AddCurriculumAsync(Curriculum curriculum);
        Task AddPostulacionAsync(Postulacion postulacion);
    }
}
