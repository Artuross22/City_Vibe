using City_Vibe.Application.Interfaces;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Implement;
using Microsoft.AspNetCore.Identity;

namespace City_Vibe.Infrastructure.Repository
{
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {

        public RoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
       
        public int UserRolesCount(string id)
        {
            var userRolesForThisRole = appDbContext.UserRoles.Where(u => u.RoleId == id).Count();
            return userRolesForThisRole;
        }
    }
}
