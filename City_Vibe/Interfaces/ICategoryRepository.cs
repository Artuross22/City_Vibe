﻿using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Category GetById(int? id);  
        
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(Category category);
        bool Save();
    }
}
