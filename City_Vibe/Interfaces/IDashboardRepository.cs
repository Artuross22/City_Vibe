using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IDashboardRepository 
    {
        Task<List<Event>> GetAllUserEvent();
        Task<List<Club>> GetAllUserClubs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
