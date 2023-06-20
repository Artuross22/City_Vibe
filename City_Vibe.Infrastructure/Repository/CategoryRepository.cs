using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Implement;


namespace City_Vibe.Infrastructure.Repository
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            
        }
    }
}
