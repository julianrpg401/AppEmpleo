using AppEmpleo.Class.Exceptions;
using AppEmpleo.Interfaces.Security;
using AppEmpleo.Interfaces.Utilities;
using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.Normalization
{
    public class UserNormalizer : IUserNormalizer
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserNormalizer(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public UserAccount NormalizeForRegistration(UserAccount input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var role = RoleConstants.Normalize(input.Role);

            if (!RoleConstants.IsValid(role))
            {
                throw new AppValidationException("Invalid role value.");
            }

            var user = new UserAccount
            {
                FirstName = TextNormalizer.CapitalizeFirst(input.FirstName),
                LastName = TextNormalizer.CapitalizeFirst(input.LastName),
                Email = NormalizeEmail(input.Email),
                PasswordHash = _passwordHasher.HashPassword(input.PasswordHash),
                Role = role,
                RegisterDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BirthDate = input.BirthDate
            };

            if (role == RoleConstants.Candidate)
            {
                user.Candidate = new Candidate();
            }
            else if (role == RoleConstants.Recruiter)
            {
                user.Recruiter = new Recruiter();
            }

            return user;
        }

        public string NormalizeEmail(string email)
        {
            return TextNormalizer.ToUpperInvariantTrim(email);
        }
    }
}
