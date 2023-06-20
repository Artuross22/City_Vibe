
using City_Vibe.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace City_Vibe.ViewModels.RoleController
{
    public class ListOfUsers
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }

        public List<AppUser> AppUsers { get; set; }

        public ListOfUsers()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
