using Microsoft.EntityFrameworkCore;
using City_Vibe.Infrastructure.Implement;
using City_Vibe.Domain.Models;
using City_Vibe.Application.Interfaces;
using City_Vibe.Infrastructure.Data;

namespace City_Vibe.Infrastructure.Repository
{
    public class AppUserRepository : GenericRepository<AppUser>,  IAppUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AppUserRepository(ApplicationDbContext _dbContext) : base(_dbContext) => dbContext = _dbContext; 
          
       public async Task<IEnumerable<AppUser>> GetUsersByRole(string roleName)
        {
            var userByRole = await(from appuser in dbContext.AppUser
                                  join userRole in dbContext.UserRoles on appuser.Id equals userRole.UserId
                                  join role in dbContext.Roles on
                                  userRole.RoleId equals role.Id
                                  where role.Name == roleName
                                  select appuser).ToListAsync();
            return userByRole;
        }

        public IQueryable<AppUser> GetAllUsersByIQueryable(string roleName)
        {
            var userByRole = (from appuser in dbContext.AppUser
                              join userRole in dbContext.UserRoles on appuser.Id equals userRole.UserId
                              join role in dbContext.Roles on
                              userRole.RoleId equals role.Id
                              where role.Name == roleName
                              select appuser);
            return userByRole;
        }

        public async Task<AppUser> GetUserByIdIncludeAdress(string id)
        {
            return await dbContext.AppUser.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
