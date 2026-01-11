using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Services
{
    public interface IOfferService
    {
        Task AddOfferAsync(JobOffer offer, UserAccount user);
        Task<List<JobOffer>> GetAllOffersAsync();
        Task<(List<JobOffer> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize);
    }
}
