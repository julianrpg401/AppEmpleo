using AppEmpleo.Class.Exceptions;
using AppEmpleo.Interfaces.Utilities;
using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.Normalization
{
    public class OfferNormalizer : IOfferNormalizer
    {
        public JobOffer NormalizeForCreation(JobOffer input, UserAccount user)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (user?.Recruiter == null)
            {
                throw new AppValidationException("Recruiter information is required to create an offer.");
            }

            return new JobOffer
            {
                RecruiterId = user.Recruiter.RecruiterId,
                JobTitle = TextNormalizer.ToUpperInvariantTrim(input.JobTitle),
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                Country = TextNormalizer.ToUpperInvariantTrim(input.Country),
                Currency = TextNormalizer.ToUpperInvariantTrim(input.Currency),
                Salary = input.Salary,
                Description = TextNormalizer.CapitalizeFirst(input.Description),
                WorkMode = TextNormalizer.ToUpperInvariantTrim(input.WorkMode),
                CategoryId = input.CategoryId
            };
        }
    }
}
