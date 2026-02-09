using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Utilities
{
    public interface IOfferNormalizer
    {
        JobOffer NormalizeForCreation(JobOffer input, UserAccount user);
    }
}
