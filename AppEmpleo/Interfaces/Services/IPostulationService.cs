using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppEmpleo.Interfaces.Services
{
    public interface IPostulationService
    {
        Task CreatePostulation(int offerId, Candidato candidate, IFormFile CVFile);
        Task<List<Postulacion>> GetAllPostulationsAsync(int recruiterId);
        Task<Curriculum?> GetCurriculumByIdAsync(int curriculumId);
        string FilePath(Curriculum curriculum);
        Task<FileResult> DownloadCurriculum(Curriculum curriculum, string filePath);
    }
}
