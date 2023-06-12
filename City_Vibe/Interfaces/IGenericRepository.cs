using City_Vibe.Models;
using System.Linq.Expressions;

namespace City_Vibe.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        public IEnumerable<TEntity> GetAll();

       public TEntity GetById<T>(T id);

        Task<TEntity?> GetByIdAsync<T>(T id);

        IQueryable<TEntity> GetQueryable();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity id);

        Task SaveChangeAsync();
    }
}
