using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();

        Task<List<Category>> FindAll();
        Category GetById(int? id);
        public IEnumerable<Category> SelectList();
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(Category category);
        bool Save();
    }
}
