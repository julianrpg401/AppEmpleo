using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class Repository<T> : IAddAsync<T> where T : class
    {
        protected readonly AppEmpleoContext _appEmpleoContext;
        private readonly DbSet<T> _dbSet;

        // Injects the database context and DbSet.
        public Repository(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
            _dbSet = appEmpleoContext.Set<T>();
        }

        // Adds an entity to the database.
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appEmpleoContext.SaveChangesAsync();
        }
    }
}
