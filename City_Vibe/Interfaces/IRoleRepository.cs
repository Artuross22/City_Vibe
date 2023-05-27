using City_Vibe.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace City_Vibe.Interfaces
{
    public interface IRoleRepository : IGenericRepository<IdentityRole>
    {
        public int UserRolesCount(string id);

    }
}
