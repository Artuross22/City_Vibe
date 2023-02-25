using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public bool Add(Category category)
        {
          
            context.Add(category);
            return Save();
               
        }

        public bool Delete(Category category)
        {
            context.Remove(category);
            return Save();
        }

        public bool Update(Category category)
        {
            context.Update(category);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public IEnumerable<Category> SelectList()
        {
            return context.Categories.ToList();
        }


        public async Task<IEnumerable<Category>> GetAll()
        {
            return await context.Categories.ToListAsync();
        }

        public Category GetById(int? id)
        {
            return context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public Task<List<Category>> FindAll()
        {
            return context.Categories.ToListAsync();
        }
    }
}
