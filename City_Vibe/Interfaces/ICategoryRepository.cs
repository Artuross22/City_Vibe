using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Category GetById(int id);  
        
        bool Add(Category club);
        bool Update(Category club);
        bool Delete(Category club);
        bool Save();
    }
}
