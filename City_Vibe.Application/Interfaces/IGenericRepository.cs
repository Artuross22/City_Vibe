﻿using System.Linq.Expressions;

namespace City_Vibe.Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        public IEnumerable<TEntity> GetAll();

       public TEntity GetById<T>(T id);

        Task<TEntity?> GetByIdAsync<T>(T id);

        IQueryable<TEntity> GetQueryable();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity id);

        bool SaveChange();
    }
}
