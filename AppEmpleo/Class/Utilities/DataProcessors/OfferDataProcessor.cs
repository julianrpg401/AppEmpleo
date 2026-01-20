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
                RecruiterId = user.Recruiter!.RecruiterId,
                JobTitle = offer.JobTitle.ToUpper(),
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Country = offer.Country.ToUpper(),
                Currency = offer.Currency.ToUpper(),
                Salary = offer.Salary,
                Description = CapitalizeFirst(offer.Description),
                WorkMode = offer.WorkMode.ToUpper(),
                CategoryId = offer.CategoryId
            };

            return offerFormatted;
        }

        // Capitaliza solo la primera letra de la descripción
        private static string CapitalizeFirst(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.Trim();
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }
    }
}
