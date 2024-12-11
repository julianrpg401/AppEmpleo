using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class OfferRepository
    {
        private readonly AppEmpleoContext _appEmpleoContext;

        public OfferRepository(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        public async Task<List<Oferta>> GetListAsync(List<Oferta>? listOffers)
        {
            try
            {
                listOffers = await _appEmpleoContext.Ofertas.ToListAsync();
                listOffers.OrderByDescending(u => u.FechaInicio);

                return listOffers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException("Error al obtener la lista de ofertas");
        }
    }
}
