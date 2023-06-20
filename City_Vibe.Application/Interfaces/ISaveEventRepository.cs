
using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface ISaveEventRepository : IGenericRepository<SaveEvent>
    {
        List<SaveEvent> FindSafeEventsinUserAndEvent(int Id);
    }
}
