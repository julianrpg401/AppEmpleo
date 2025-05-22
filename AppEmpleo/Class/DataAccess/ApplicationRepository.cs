using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppEmpleo.Class.DataAccess
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppEmpleoContext _context;

        public ApplicationRepository(AppEmpleoContext context)
        {
            _context = context;
        }

        public async Task<List<Postulacion>> GetAllPostulacionesAsync()
        {
            return await _context.Postulaciones
                .Include(p => p.Curriculum)
                    .ThenInclude(c => c.Candidato)
                .Include(p => p.OfertaEmpleo)
                .ToListAsync();
        }

        public async Task<Curriculum?> GetCurriculumByIdAsync(int curriculumId)
        {
            return await _context.Curriculums
                .FirstOrDefaultAsync(c => c.CurriculumId == curriculumId);
        }

        public async Task AddCurriculumAsync(Curriculum curriculum)
        {
            _context.Curriculums.Add(curriculum);
            await _context.SaveChangesAsync();
        }

        public async Task AddPostulacionAsync(Postulacion postulacion)
        {
            _context.Postulaciones.Add(postulacion);
            await _context.SaveChangesAsync();
        }
    }
}
