using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppEmpleo.Class.DataAccess
{
    public class OfferRepository : Repository<JobOffer>, IOfferRepository
    {
        // Pasa el contexto a la clase base
        public OfferRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext) { }

        // Obtiene todas las ofertas de la base de datos
        public async Task<List<JobOffer>> GetOffersAsync()
        {
            try
            {
                var listOffers = await _appEmpleoContext.JobOffers
                    .OrderByDescending(o => o.StartDate)
                    .Include(r => r.Recruiter!.User.FirstName)
                    .ToListAsync();

                return listOffers;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener la lista de ofertas");
                throw;
            }
        }

        // Obtiene ofertas paginadas
        public async Task<(List<JobOffer> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = _appEmpleoContext.JobOffers
                    .OrderByDescending(o => o.StartDate)
                    .Include(r => r.Recruiter!.User);

                var totalCount = await query.CountAsync();
                var offers = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                foreach (var offer in offers)
                {
                    Log.Information("Oferta obtenida: {JobTitle} por {FirstName}", offer.JobTitle, offer.Recruiter?.User.FirstName);
                    offer.Description.ToLower();
                }

                return (offers, totalCount);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener la lista paginada de ofertas");
                throw;
            }
        }
    }
}
