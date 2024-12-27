using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.DataAccess
{
    public class Repository<T> : IAddAsync<T> where T : class
    {
        protected readonly AppEmpleoContext _appEmpleoContext;
        private readonly DbSet<T> _dbSet;

        // Inyecta el contexto de la base de datos y un DbSet (de un modelo)
        public Repository(AppEmpleoContext appEmpleoContext)
        {
            _appEmpleoContext = appEmpleoContext;
            _dbSet = appEmpleoContext.Set<T>();
        }

        // Añade un objeto a la base de datos
        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _appEmpleoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\nError al insertar en la BD");
            }
        }
    }
}
