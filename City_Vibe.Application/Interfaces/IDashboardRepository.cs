

using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface IDashboardRepository 
    {
        Task<List<Event>> GetAllUserEvent();

        Task<List<Club>> GetAllUserClubs();
    }
}
