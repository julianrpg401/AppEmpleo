using AppEmpleo.Interfaces;
using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Interfaces.Services;
using Serilog;
using AppEmpleo.Models;
using AppEmpleo.Class.Utilities.DataProcessors;

namespace AppEmpleo.Class.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task AddOfferAsync(JobOffer offer, UserAccount user)
        {
            try
            {
                offer = OfferDataProcessor.OfferFormat(offer, user);
                await _offerRepository.AddAsync(offer);
            }
            catch
            {
                Log.Error("Error al añadir la oferta: {Offer}", offer);
                throw new Exception("Error al añadir la oferta. Por favor, inténtelo de nuevo más tarde.");
            }
            finally
            {
                Log.Information("Oferta añadida correctamente: {Offer}", offer);
            }
        }

        public async Task<List<JobOffer>> GetAllOffersAsync()
        {
            return await _offerRepository.GetOffersAsync();
        }

        public async Task<(List<JobOffer> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize)
        {
            return await _offerRepository.GetOffersPagedAsync(pageNumber, pageSize);
        }
    }
}
