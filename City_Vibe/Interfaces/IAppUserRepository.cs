using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);

        Task<IEnumerable<AppUser>> GetUsersByRole(string role);

        IQueryable<AppUser> GetAllUsersByIQueryable(string role);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
