using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IDashboardRepository 
    {
        Task<List<Event>> GetAllUserEvent();

        Task<List<Club>> GetAllUserClubs();
    }
}
