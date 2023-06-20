using City_Vibe.Application.Interfaces;
using City_Vibe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace City_Vibe.Infrastructure.Implement
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {  
        protected readonly ApplicationDbContext appDbContext;

        public DbSet<TEntity> Entities => appDbContext.Set<TEntity>();

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            appDbContext = applicationDbContext;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
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
