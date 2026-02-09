using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IJobApplicationRepository
    {
        Task<List<JobApplication>> GetApplicationsByRecruiterUserIdAsync(int recruiterUserId);
        Task<Resume?> GetResumeByIdAsync(int resumeId);
        Task AddResumeAsync(Resume resume);
        Task AddApplicationAsync(JobApplication application);
    }
}
