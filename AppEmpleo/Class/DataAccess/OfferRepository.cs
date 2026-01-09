using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppEmpleo.Class.DataAccess
{
    public class OfferRepository : Repository<Oferta>, IOfferRepository
    {
        // Pasa el contexto a la clase base
        public OfferRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext) { }

        // Obtiene todas las ofertas de la base de datos
        public async Task<List<Oferta>> GetOffersAsync()
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
                Log.Error(ex, "Error al obtener la lista de ofertas");
                throw;
            }
        }

        // Obtiene ofertas paginadas
        public async Task<(List<Oferta> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = _appEmpleoContext.Ofertas.OrderByDescending(o => o.FechaInicio);
                var totalCount = await query.CountAsync();
                var offers = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                return (offers, totalCount);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener la lista paginada de ofertas");
                throw;
            }
        }
    }
}
