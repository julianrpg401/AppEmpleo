using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Repositories
{
    public interface IOfferRepository : IAddAsync<Oferta>
    {
        Task<List<Oferta>> GetOffersAsync();
    }
}
