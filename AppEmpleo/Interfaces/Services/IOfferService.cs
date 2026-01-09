using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Services
{
    public interface IOfferService
    {
        Task AddOfferAsync(Oferta offer, Usuario user);
        Task<List<Oferta>> GetAllOffersAsync();
        Task<(List<Oferta> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize);
    }
}
