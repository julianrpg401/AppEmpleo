using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.DataProcessors
{
    public class OfferDataProcessor
    {
        // Formatea una oferta para ser insertada en la base de datos
        public static JobOffer OfferFormat(JobOffer offer, UserAccount user)
        {
            JobOffer offerFormatted = new JobOffer()
            {
                RecruiterId = user.UserId,
                JobTitle = offer.JobTitle.ToUpper(),
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Country = offer.Country.ToUpper(),
                Currency = offer.Currency.ToUpper(),
                Salary = offer.Salary,
                Description = offer.Description.ToUpper(),
                WorkMode = offer.WorkMode.ToUpper(),
                CategoryId = offer.CategoryId
            };

            return offerFormatted;
        }
    }
}
