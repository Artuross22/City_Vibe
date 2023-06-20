using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {

        Task< AppUser> GetUserByIdIncludeAdress(string id);

        Task<IEnumerable<AppUser>> GetUsersByRole(string role);

        IQueryable<AppUser> GetAllUsersByIQueryable(string role);
     
    }
}
