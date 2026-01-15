using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.DataProcessors
{
    public class CurriculumDataProcessor
    {
        // Formatea un currículum a partir de un candidato y un archivo CV
        public static Resume CurriculumFormat(Candidate candidate, IFormFile CVFile, string path)
        {
            Resume curriculumFormatted = new Resume()
            {
                CandidateId = candidate.CandidateId,
                FileName = CVFile.FileName.ToUpper(),
                FilePath = $"/curriculums/{path}",
                UploadedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                IsPreferred = false
            };

            return curriculumFormatted;
        }
    }
}
