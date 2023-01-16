using Microsoft.EntityFrameworkCore;
using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;

namespace City_Vibe.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppUserRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public bool Add(AppUser user)
        {
            _dbContext.Add(user);
            return Save();
        }

        public bool Update(AppUser user)
        {
            _dbContext.Update(user);
            return Save();
        }

        public bool Delete(AppUser user)
        {
            _dbContext.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

       public async Task<IEnumerable<AppUser>> GetUsersByRole(string roleName)
        {
            var userByRole = await(from appuser in _dbContext.AppUser
                                  join userRole in _dbContext.UserRoles on appuser.Id equals userRole.UserId
                                  join role in _dbContext.Roles on
                                  userRole.RoleId equals role.Id
                                  where role.Name == roleName
                                  select appuser).ToListAsync();

            return userByRole;
        }



        public async Task<AppUser> GetUserById(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public IQueryable<AppUser> GetAllUsersByIQueryable(string roleName)
        {
            var userByRole = (from appuser in _dbContext.AppUser
                                   join userRole in _dbContext.UserRoles on appuser.Id equals userRole.UserId
                                   join role in _dbContext.Roles on
                                   userRole.RoleId equals role.Id
                                   where role.Name == roleName
                                   select appuser);

            return userByRole;
        }
    }
}
