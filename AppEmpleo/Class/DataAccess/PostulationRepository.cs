using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppEmpleo.Class.DataAccess
{
    public class PostulationRepository : Repository<Postulacion>, IPostulationRepository
    {
        // Contexto de la base de datos
        public PostulationRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext) { }

        // Método para obtener todas las postulaciones
        public async Task<List<Postulacion>> GetAllPostulationsAsync()
        {
            try
            {
                return await _appEmpleoContext.Postulaciones
                    .Include(p => p.Candidato)
                    .ThenInclude(c => c.Usuario)
                    .Include(p => p.OfertaEmpleo)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todas las postulaciones");
                throw new Exception("Error al obtener las postulaciones", ex);
            }
        }

        // Método para obtener las postulaciones de un reclutador específico
        public async Task<List<Postulacion>> GetPostulationsByRecruiterIdAsync(int recruiterId)
        {
            try
            {
                return await _appEmpleoContext.Postulaciones
                    .Include(p => p.Candidato)
                    .ThenInclude(c => c.Usuario)
                    .Include(p => p.OfertaEmpleo)
                    .ThenInclude(o => o.Reclutador)
                    .Where(p => p.OfertaEmpleo.ReclutadorId == recruiterId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener las postulaciones del reclutador con ID {RecruiterId}", recruiterId);
                throw new Exception("Error al obtener las postulaciones del reclutador", ex);
            }
        }

        // Método para obtener un curriculum por su ID
        public async Task<Curriculum?> GetCurriculumByIdAsync(int curriculumId)
        {
            try
            {
                return await _appEmpleoContext.Curriculums
                    .FirstOrDefaultAsync(c => c.CurriculumId == curriculumId);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el curriculum con ID {CurriculumId}", curriculumId);
                throw new Exception("Error al obtener el curriculum", ex);
            }
        }

        // Método para añadir un curriculum a la base de datos
        public async Task AddCurriculumAsync(Curriculum curriculum)
        {
            try
            {
                await _appEmpleoContext.Curriculums.AddAsync(curriculum);
                await _appEmpleoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al añadir el curriculum");
                throw new Exception("Error al añadir el curriculum", ex);
            }
        }

        // Método para añadir una postulación a la base de datos
        public async Task AddPostulationAsync(Postulacion postulation)
        {
            try
            {
                await _appEmpleoContext.Postulaciones.AddAsync(postulation);
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
