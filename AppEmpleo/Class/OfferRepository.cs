using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class
{
    public class OfferRepository
    {
        private readonly AppEmpleoContext _appEmpleoContext;

        public OfferRepository(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
        }

        public async Task<List<Oferta>> GetListAsync(List<Oferta> listaOfertas)
        {
            try
            {
                listaOfertas = await _appEmpleoContext.Ofertas.ToListAsync();
                listaOfertas.OrderByDescending(u => u.FechaInicio);

                return listaOfertas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException("Error al obtener la lista de ofertas");
        }
    }
}
