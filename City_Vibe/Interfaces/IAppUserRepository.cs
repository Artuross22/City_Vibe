using City_Vibe.Models;
using NuGet.ProjectModel;

namespace City_Vibe.Interfaces
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {

        Task< AppUser> GetUserByIdIncludeAdress(string id);

        Task<IEnumerable<AppUser>> GetUsersByRole(string role);

        IQueryable<AppUser> GetAllUsersByIQueryable(string role);
     
    }
}
