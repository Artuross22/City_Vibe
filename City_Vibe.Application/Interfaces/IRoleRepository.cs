using Microsoft.AspNetCore.Identity;


namespace City_Vibe.Application.Interfaces
{
    public interface IRoleRepository : IGenericRepository<IdentityRole>
    {
        public int UserRolesCount(string id);

    }
}
