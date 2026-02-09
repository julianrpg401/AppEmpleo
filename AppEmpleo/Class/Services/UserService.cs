using AppEmpleo.Class.Utilities.Normalization;
using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Interfaces.Security;
using AppEmpleo.Interfaces.Services;
using AppEmpleo.Interfaces.Services.SessionServices;
using AppEmpleo.Interfaces.Utilities;
using AppEmpleo.Models;

namespace AppEmpleo.Class.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClaimsService _claimsService;
        private readonly IUserNormalizer _userNormalizer;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IClaimsService claimsService,
            IUserNormalizer userNormalizer,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _claimsService = claimsService;
            _userNormalizer = userNormalizer;
            _passwordHasher = passwordHasher;
        }

        // Registers a new user.
        public async Task<UserAccount?> RegisterUserAsync(UserAccount user)
        {
            var normalized = _userNormalizer.NormalizeForRegistration(user);
            var exists = await _userRepository.EmailExistsAsync(normalized.Email);

            if (exists)
            {
                return null;
            }

            await _userRepository.AddAsync(normalized);
            return normalized;
        }

        // Authenticates a user.
        public async Task<bool> LoginAsync(string email, string password)
        {
            var normalizedEmail = _userNormalizer.NormalizeEmail(email);
            var existingUser = await _userRepository.GetByEmailAsync(normalizedEmail);

            if (existingUser == null)
            {
                return false;
            }

            if (!_passwordHasher.VerifyPassword(password, existingUser.PasswordHash))
            {
                return false;
            }

            await _claimsService.UserLogin(existingUser);
            return true;
        }

        // Gets the current user from claims.
        public UserAccount GetCurrentUserFromClaims()
        {
            var role = _claimsService.GetRole();
            var user = new UserAccount
            {
                UserId = _claimsService.GetId(),
                FirstName = _claimsService.GetName(),
                Email = _claimsService.GetEmail(),
                Role = role,
                LastName = string.Empty,
                PasswordHash = string.Empty,
                BirthDate = default,
                RegisterDate = default
            };

            if (role == RoleConstants.Candidate)
            {
                user.Candidate = new Candidate
                {
                    CandidateId = int.Parse(_claimsService.GetUserData())
                };
            }
            else if (role == RoleConstants.Recruiter)
            {
                user.Recruiter = new Recruiter
                {
                    RecruiterId = int.Parse(_claimsService.GetUserData())
                };
            }

            return user;
        }

        // Retrieves the candidate entity by user id.
        public async Task<Candidate?> GetCandidateByUserIdAsync(int userId)
        {
            return await _userRepository.GetCandidateByUserIdAsync(userId);
        }
    }
}
