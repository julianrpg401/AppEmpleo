using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.DataProcessors
{
    public class CurriculumDataProcessor
    {
        // Formatea un currículum a partir de un candidato y un archivo CV
        public static Curriculum CurriculumFormat(Candidato candidate, IFormFile CVFile, string path)
        {
            Curriculum curriculumFormatted = new Curriculum()
            {
                CandidatoId = candidate.CandidatoId,
                NombreArchivo = CVFile.FileName.ToUpper(),
                RutaArchivo = $"/curriculums/{path}",
                FechaCarga = DateOnly.FromDateTime(DateTime.UtcNow),
                EsPreferido = false
            };

            return curriculumFormatted;
        }
    }
}
