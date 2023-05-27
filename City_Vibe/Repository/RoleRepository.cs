using City_Vibe.Data;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace City_Vibe.Repository
{
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {

        public RoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public int UserRolesCount(string id)
        {
            var userRolesForThisRole = appDbContext.UserRoles.Where(u => u.RoleId == id).Count();
            return userRolesForThisRole;
        }
    }
}
