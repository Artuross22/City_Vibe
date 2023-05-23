using City_Vibe.Data;
using City_Vibe.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Implement
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext appDbContext;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            appDbContext = applicationDbContext;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await appDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync<T>(T id)
        {
            return await appDbContext.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return appDbContext.Set<TEntity>();
        }


        public void Add(TEntity entity)
        {
            appDbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity id)
        {
            appDbContext.Set<TEntity>().Remove(id);
        }

        public void Update(TEntity entity)
        {
            appDbContext.Set<TEntity>().Update(entity);
        }

        public Task SaveChangeAsync()
        {
            return appDbContext.SaveChangesAsync();
        }
    }
}
