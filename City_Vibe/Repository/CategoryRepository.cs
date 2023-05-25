using City_Vibe.Data;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace City_Vibe.Repository
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            
        }
    }
}
