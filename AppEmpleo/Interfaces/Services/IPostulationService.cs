using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppEmpleo.Interfaces.Services
{
    public interface IPostulationService
    {
        Task CreatePostulation(int offerId, Candidate candidate, IFormFile CVFile);
        Task<List<JobApplication>> GetAllPostulationsAsync(int recruiterId);
        Task<Resume?> GetCurriculumByIdAsync(int curriculumId);
        string FilePath(Resume curriculum);
        Task<FileResult> DownloadCurriculum(Resume curriculum, string filePath);
    }
}
