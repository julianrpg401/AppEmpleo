using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IOfferRepository : IAddAsync<Oferta>
    {
        Task<List<Oferta>> GetOffersAsync();
        Task<(List<Oferta> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize);
    }
}
