using City_Vibe.Data;
using City_Vibe.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace City_Vibe.Implement
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {  
        protected readonly ApplicationDbContext appDbContext;

        public DbSet<TEntity> Entities => appDbContext.Set<TEntity>();

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            appDbContext = applicationDbContext;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
             return Entities.ToList();         
        }

        public TEntity GetById<T>(T id)
        {
            return Entities.Find(id);
        }

        public async Task<TEntity?> GetByIdAsync<T>(T id)
        {
            return await Entities.FindAsync(id);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return Entities;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }


        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void Delete(TEntity id)
        {
            Entities.Remove(id);
        }

        public void Update(TEntity entity)
        {
            Entities.Update(entity);
        }

        public Task SaveChangeAsync()
        {
            return appDbContext.SaveChangesAsync();
        }

    }
}
