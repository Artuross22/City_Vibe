using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface ISaveEventRepository : IGenericRepository<SaveEvent>
    {
        Task<IEnumerable<SaveEvent>> FindEventByIdAsync(int Id);
        List<SaveEvent> FindSafeEventsinUserAndEvent(int Id);
    }
}
