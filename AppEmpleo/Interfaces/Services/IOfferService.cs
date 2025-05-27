using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Services
{
    public interface IOfferService
    {
        Task AddOfferAsync(Oferta offer, Usuario user);
        Task<List<Oferta>> GetAllOffersAsync();
    }
}
