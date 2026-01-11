using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IOfferRepository : IAddAsync<JobOffer>
    {
        Task<List<JobOffer>> GetOffersAsync();
        Task<(List<JobOffer> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize);
    }
}
