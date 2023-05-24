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

      //  private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            
        }

        //public bool Add(Category category)
        //{

        //    appDbContext.Add(category);
        //    return Save();

        //}

        //public bool Delete(Category category)
        //{
        //    appDbContext.Remove(category);
        //    return Save();
        //}

        //public bool Update(Category category)
        //{
        //    appDbContext.Update(category);
        //    return Save();
        //}

        //public bool Save()
        //{
        //    var saved = appDbContext.SaveChanges();
        //    return saved > 0 ? true : false;
        //}


        //public IEnumerable<Category> SelectList()
        //{
        //    return appDbContext.Categories.ToList();
        //}


        //public async Task<IEnumerable<Category>> GetAll()
        //{
        //    return await appDbContext.Categories.ToListAsync();
        //}

        //public Category GetById(int? id)
        //{
        //    return appDbContext.Categories.FirstOrDefault(x => x.Id == id);
        //}

        //public Task<List<Category>> FindAll()
        //{
        //    return appDbContext.Categories.ToListAsync();
        //}
    }
}
