using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class UserRepository : Repository<UserAccount>, IUserRepository
    {
        // Passes the context to the base class.
        public UserRepository(AppEmpleoContext appEmpleoContext) : base(appEmpleoContext) { }

        // Checks whether an email is already registered.
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _appEmpleoContext.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);
        }

        // Retrieves a user with role-specific navigation properties.
        public async Task<UserAccount?> GetByEmailAsync(string email)
        {
            return await _appEmpleoContext.Users
                .AsNoTracking()
                .Include(u => u.Candidate)
                .Include(u => u.Recruiter)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Obtiene un usuario con el rol y su respectivo id

        // Gets a candidate by user id.
        public async Task<Candidate?> GetCandidateByUserIdAsync(int userId)
        {
            return await _appEmpleoContext.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
