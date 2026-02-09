using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class OfferRepository : Repository<JobOffer>, IOfferRepository
    {
        // Passes the context to the base class.
        public OfferRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext) { }

        // Retrieves offers from the database.
        public async Task<List<JobOffer>> GetOffersAsync()
        {
            return await _appEmpleoContext.JobOffers
                .AsNoTracking()
                .OrderByDescending(o => o.StartDate)
                .Include(o => o.Recruiter)
                .ThenInclude(r => r!.User)
                .ToListAsync();
        }

        // Retrieves paged offers.
        public async Task<(List<JobOffer> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize)
        {
            var query = _appEmpleoContext.JobOffers
                .AsNoTracking()
                .OrderByDescending(o => o.StartDate)
                .Include(r => r.Recruiter)
                .ThenInclude(r => r!.User);

            var totalCount = await query.CountAsync();
            var offers = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (offers, totalCount);
        }
    }
}
