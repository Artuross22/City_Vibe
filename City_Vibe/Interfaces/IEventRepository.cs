using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetByIdAsync(int id);
        Task<Event> GetByIdAsyncNoTracking(int id);


        bool Add(Event eventAdd);
        bool Update(Event eventUp);
        bool Delete(Event eventDelete);
        bool Save();
    }
}
