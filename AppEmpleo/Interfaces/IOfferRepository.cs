using AppEmpleo.Models;

namespace AppEmpleo.Interfaces
{
    public interface IOfferRepository : IAddAsync<Oferta>, IGetOfferAsync
    {
    }
}
