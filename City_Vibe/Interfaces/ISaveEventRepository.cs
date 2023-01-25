using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ISaveEventRepository
    {
        ICollection<SaveEvent> FindEventById(string Id);
        Task<IEnumerable<SaveEvent>> FindEventByIdAsync(int Id);
        Task<SaveEvent> FindEventById(int Id);
        List<SaveEvent> FindSafeEventsinUserAndEvent(int Id);
        bool Add(SaveEvent saveEventAdd);
        bool Update(SaveEvent saveEventUp);
        bool Delete(SaveEvent saveEventDelete);
        bool Save();
    }
}
