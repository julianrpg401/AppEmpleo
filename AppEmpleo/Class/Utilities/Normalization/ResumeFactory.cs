using AppEmpleo.Interfaces.Utilities;
using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.Normalization
{
    public class ResumeFactory : IResumeFactory
    {
        public Resume Create(Candidate candidate, string originalFileName, string storedFileName)
        {
            if (candidate == null)
            {
                throw new ArgumentNullException(nameof(candidate));
            }

            return new Resume
            {
                CandidateId = candidate.CandidateId,
                FileName = string.IsNullOrWhiteSpace(originalFileName)
                    ? storedFileName
                    : Path.GetFileName(originalFileName),
                FilePath = $"/curriculums/{storedFileName}",
                UploadedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                IsPreferred = false
            };
        }
    }
}
