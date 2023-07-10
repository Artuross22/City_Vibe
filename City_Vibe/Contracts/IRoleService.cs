using City_Vibe.Domain.Models;
using City_Vibe.Services.Base;
using City_Vibe.ViewModels.RoleController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Contracts
{
    public interface IRoleService
    {
        IEnumerable<IdentityRole> Index();

        IdentityRole UpsertGet(string id);

        Task<Response> UpsertPost(IdentityRole role);

        Task<Response> Delete(string id);

        Task<ChangeRoleViewModel> EditUserRoleGet(string userId);

        Task<Response> EditUserRolePost(string userId, List<string> roles);

       IEnumerable<AppUser> UserList();
    }
}
