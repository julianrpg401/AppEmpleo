﻿using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class OfferRepository : Repository<Oferta>, IOfferRepository
    {
        // Pasa el contexto a la clase base
        public OfferRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext)
        {
        }

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
                Console.WriteLine(ex.Message);
            }

            throw new ArgumentException("Error al obtener la lista de ofertas");
        }
    }
}
