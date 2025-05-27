using AppEmpleo.Class.Utilities.DataProcessors;
using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Interfaces.Services;
using AppEmpleo.Models;
using Serilog;

namespace AppEmpleo.Class.Services
{
    public class PostulationService : IPostulationService
    {
        private readonly IPostulationRepository _postulationRepository;
        private readonly IWebHostEnvironment _env;

        public PostulationService(IPostulationRepository postulationRepository, IWebHostEnvironment webHostEnvironment)
        {
            _postulationRepository = postulationRepository;
            _env = webHostEnvironment;
        }

        // Crea un currículum y lo guarda en el servidor
        private async Task<int> CreateCurriculum(Candidato candidate, IFormFile CVFile)
        {
            try
            {
                var path = await SaveCurriculum(CVFile);
                var curriculum = CurriculumDataProcessor.CurriculumFormat(candidate, CVFile, path);

                await SendCurriculum(curriculum);

                return curriculum.CurriculumId;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear el currículum");
                throw new Exception("Ocurrió un error al crear el currículum. Por favor, inténtelo de nuevo más tarde.");
            }
        }

        // Guarda el archivo del currículum en el servidor y devuelve el nombre único del archivo
        private async Task<string> SaveCurriculum(IFormFile CVFile)
        {
            try
            {
                var folder = Path.Combine(_env.WebRootPath, "curriculums");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(CVFile.FileName)}";
                var savePath = Path.Combine(folder, uniqueName);

                using var stream = new FileStream(savePath, FileMode.Create);

                await CVFile.CopyToAsync(stream);

                return uniqueName;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al guardar el currículum");
                throw new Exception("Ocurrió un error al guardar el currículum. Por favor, inténtelo de nuevo más tarde.");
            }
        }

        // Envía el currículum al repositorio para guardarlo en la base de datos
        private async Task SendCurriculum(Curriculum curriculum)
        {
            try
            {
                await _postulationRepository.AddCurriculumAsync(curriculum);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al enviar el currículum");
                throw new Exception("Ocurrió un error al enviar el currículum. Por favor, inténtelo de nuevo más tarde.");
            }
        }

        // Crea una postulación para una oferta de empleo
        public async Task CreatePostulation(int offerId, Candidato candidate, IFormFile CVFile)
        {
            try
            {
                var postulacion = new Postulacion
                {
                    OfertaEmpleoId = offerId,
                    CandidatoId = candidate.CandidatoId,
                    CurriculumId = await CreateCurriculum(candidate, CVFile), // crea el currículum y obtiene su ID
                    FechaPostulacion = DateTime.UtcNow
                };

                await _postulationRepository.AddPostulationAsync(postulacion);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear la postulación");
                throw new Exception("Ocurrió un error al crear la postulación. Por favor, inténtelo de nuevo más tarde.");
            }
        }

        // Obtiene todas las postulaciones de un reclutador por su ID
        public async Task<List<Postulacion>> GetAllPostulationsAsync(int recruiterId)
        {
            try
            {
                return await _postulationRepository.GetPostulationsByRecruiterIdAsync(recruiterId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener las postulaciones del reclutador con ID {RecruiterId}", recruiterId);
                throw new Exception("Ocurrió un error al obtener las postulaciones. Por favor, inténtelo de nuevo más tarde.");
            }
        }
    }
}
