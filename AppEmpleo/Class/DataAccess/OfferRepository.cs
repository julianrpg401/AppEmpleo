using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class OfferRepository
    {
        private readonly AppEmpleoContext _appEmpleoContext;

        // Inyectar la base de datos
        public OfferRepository(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        // Devuelve todas las ofertas de la base de datos
        public async Task<List<Oferta>?> GetOffersAsync()
        {
            try
            {
                var listOffers = await _appEmpleoContext.Ofertas
                    .OrderByDescending(o => o.FechaInicio)
                    .ToListAsync();

                return listOffers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException("Error al obtener la lista de ofertas");
        }

        public async Task AddOfferAsync(Oferta offer)
        {
            await _appEmpleoContext.Ofertas.AddAsync(offer);
            await _appEmpleoContext.SaveChangesAsync();
        }
    }
}
