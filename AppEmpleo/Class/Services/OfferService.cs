using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Interfaces.Services;
using AppEmpleo.Interfaces.Utilities;
using AppEmpleo.Models;

namespace AppEmpleo.Class.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IOfferNormalizer _offerNormalizer;

        public OfferService(IOfferRepository offerRepository, IOfferNormalizer offerNormalizer)
        {
            _offerRepository = offerRepository;
            _offerNormalizer = offerNormalizer;
        }

        public async Task AddOfferAsync(JobOffer offer, UserAccount user)
        {
            var normalized = _offerNormalizer.NormalizeForCreation(offer, user);
            await _offerRepository.AddAsync(normalized);
        }

        public async Task<(List<JobOffer> Offers, int TotalCount)> GetOffersPagedAsync(int pageNumber, int pageSize)
        {
            return await _offerRepository.GetOffersPagedAsync(pageNumber, pageSize);
        }
    }
}
