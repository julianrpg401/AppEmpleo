using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class JobApplicationRepository : Repository<JobApplication>, IJobApplicationRepository
    {
        // Database context.
        public JobApplicationRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext) { }

        // Retrieves applications for a recruiter user id.
        public async Task<List<JobApplication>> GetApplicationsByRecruiterUserIdAsync(int recruiterUserId)
        {
            return await _appEmpleoContext.JobApplications
                .AsNoTracking()
                .Include(p => p.Candidate)
                .ThenInclude(c => c.User)
                .Include(p => p.JobOffer)
                .ThenInclude(o => o.Recruiter)
                .Where(p => p.JobOffer.Recruiter != null && p.JobOffer.Recruiter.UserId == recruiterUserId)
                .ToListAsync();
        }

        // Retrieves a resume by id.
        public async Task<Resume?> GetResumeByIdAsync(int resumeId)
        {
            return await _appEmpleoContext.Resumes
                .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ResumeId == resumeId);
        }

        // Adds a resume to the database.
        public async Task AddResumeAsync(Resume resume)
        {
            await _appEmpleoContext.Resumes.AddAsync(resume);
            await _appEmpleoContext.SaveChangesAsync();
        }

        // Adds a job application to the database.
        public async Task AddApplicationAsync(JobApplication application)
        {
            await _appEmpleoContext.JobApplications.AddAsync(application);
            await _appEmpleoContext.SaveChangesAsync();
        }
    }
}
