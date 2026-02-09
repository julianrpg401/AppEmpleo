using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppEmpleo.Interfaces.Services
{
    public interface IJobApplicationService
    {
        Task CreateApplicationAsync(int offerId, Candidate candidate, IFormFile file);
        Task<List<JobApplication>> GetApplicationsByRecruiterAsync(int recruiterUserId);
        Task<Resume?> GetResumeByIdAsync(int resumeId);
        string GetResumeFilePath(Resume resume);
        Task<FileResult> DownloadResumeAsync(Resume resume, string filePath);
    }
}
