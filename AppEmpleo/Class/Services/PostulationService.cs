using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Interfaces.Services;
using AppEmpleo.Interfaces.Services.Storage;
using AppEmpleo.Interfaces.Utilities;
using AppEmpleo.Models;
using AppEmpleo.Class.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AppEmpleo.Class.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _applicationRepository;
        private readonly IResumeStorage _resumeStorage;
        private readonly IResumeFactory _resumeFactory;

        public JobApplicationService(
            IJobApplicationRepository applicationRepository,
            IResumeStorage resumeStorage,
            IResumeFactory resumeFactory)
        {
            _applicationRepository = applicationRepository;
            _resumeStorage = resumeStorage;
            _resumeFactory = resumeFactory;
        }

        private static void EnsurePdf(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
            {
                throw new AppValidationException("Only PDF files are allowed.");
            }
        }

        // Creates a resume record and stores the file.
        private async Task<int> CreateResumeAsync(Candidate candidate, IFormFile file)
        {
            EnsurePdf(file);
            var storedFileName = await _resumeStorage.SaveAsync(file);
            var resume = _resumeFactory.Create(candidate, file.FileName, storedFileName);
            await _applicationRepository.AddResumeAsync(resume);
            return resume.ResumeId;
        }

        // Creates a job application for an offer.
        public async Task CreateApplicationAsync(int offerId, Candidate candidate, IFormFile file)
        {
            var application = new JobApplication
            {
                JobOfferId = offerId,
                CandidateId = candidate.CandidateId,
                ResumeId = await CreateResumeAsync(candidate, file),
                AppliedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await _applicationRepository.AddApplicationAsync(application);
        }

        // Retrieves recruiter applications.
        public async Task<List<JobApplication>> GetApplicationsByRecruiterAsync(int recruiterUserId)
        {
            return await _applicationRepository.GetApplicationsByRecruiterUserIdAsync(recruiterUserId);
        }

        // Retrieves a resume by id.
        public async Task<Resume?> GetResumeByIdAsync(int resumeId)
        {
            return await _applicationRepository.GetResumeByIdAsync(resumeId);
        }

        // Builds the absolute path for a stored resume.
        public string GetResumeFilePath(Resume resume)
        {
            var storedFileName = Path.GetFileName(resume.FilePath);
            return _resumeStorage.GetAbsolutePath(storedFileName);
        }

        // Downloads the resume from disk.
        public async Task<FileResult> DownloadResumeAsync(Resume resume, string filePath)
        {
            var contentType = "application/pdf";
            var fileName = resume.FileName;
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return new FileContentResult(fileBytes, contentType)
            {
                FileDownloadName = fileName
            };
        }
    }
}
