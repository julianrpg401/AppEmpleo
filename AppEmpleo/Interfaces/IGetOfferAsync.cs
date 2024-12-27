using AppEmpleo.Models;

namespace AppEmpleo.Interfaces
{
    public interface IGetOfferAsync
    {
        Task<List<Oferta>> GetOffersAsync();
    }
}
