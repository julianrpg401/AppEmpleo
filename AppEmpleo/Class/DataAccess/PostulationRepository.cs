using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppEmpleo.Class.DataAccess
{
    public class PostulationRepository : Repository<JobApplication>, IPostulationRepository
    {
        // Contexto de la base de datos
        public PostulationRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext) { }

        // Método para obtener todas las postulaciones
        public async Task<List<JobApplication>> GetAllPostulationsAsync()
        {
            try
            {
                return await _appEmpleoContext.JobApplications
                    .Include(p => p.Candidate)
                    .ThenInclude(c => c.User)
                    .Include(p => p.JobOffer)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todas las postulaciones");
                throw new Exception("Error al obtener las postulaciones", ex);
            }
        }

        // Método para obtener las postulaciones de un reclutador específico
        public async Task<List<JobApplication>> GetPostulationsByRecruiterIdAsync(int recruiterId)
        {
            try
            {
                return await _appEmpleoContext.JobApplications
                    .Include(p => p.Candidate)
                    .ThenInclude(c => c.User)
                    .Include(p => p.JobOffer)
                    .ThenInclude(o => o.Recruiter)
                    // NOTE: Historically this method was called with the authenticated user's UsuarioId.
                    // Filtering by Recruiter.UserId keeps it correct even if RecruiterId != UserId.
                        .Where(p => p.JobOffer.Recruiter != null && p.JobOffer.Recruiter.UserId == recruiterId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener las postulaciones del reclutador con ID {RecruiterId}", recruiterId);
                throw new Exception("Error al obtener las postulaciones del reclutador", ex);
            }
        }

        // Método para obtener un curriculum por su ID
        public async Task<Resume?> GetCurriculumByIdAsync(int curriculumId)
        {
            try
            {
                return await _appEmpleoContext.Resumes
                    .FirstOrDefaultAsync(c => c.ResumeId == curriculumId);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el curriculum con ID {CurriculumId}", curriculumId);
                throw new Exception("Error al obtener el curriculum", ex);
            }
        }

        // Método para añadir un curriculum a la base de datos
        public async Task AddCurriculumAsync(Resume curriculum)
        {
            try
            {
                await _appEmpleoContext.Resumes.AddAsync(curriculum);
                await _appEmpleoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al añadir el curriculum");
                throw new Exception("Error al añadir el curriculum", ex);
            }
        }

        // Método para añadir una postulación a la base de datos
        public async Task AddPostulationAsync(JobApplication postulation)
        {
            try
            {
                await _appEmpleoContext.JobApplications.AddAsync(postulation);
                await _appEmpleoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al añadir la postulacion");
                throw new Exception("Error al añadir la postulacion", ex);
            }
        }
    }
}
