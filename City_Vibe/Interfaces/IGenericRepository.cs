﻿using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        public IEnumerable<Category> GetAll();

       public Category GetById(int id);

        Task<TEntity?> GetByIdAsync<T>(T id);

        IQueryable<TEntity> GetQueryable();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity id);

        Task SaveChangeAsync();
    }
}