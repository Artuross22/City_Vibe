using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AppUserController;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Contracts
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUserViewModel>> Index();

        Task<AppUserDetailViewModel> Detail(string id);

        Task<EditProfileViewModel> EditProfileGet(AppUser user);

        Task<EditProfileViewModel> EditProfilePost(EditProfileViewModel editVM, AppUser user);

        Task<AppUserClaimsViewModel> ManageClaimsGet(AppUser user);

        Task<AppUserClaimsViewModel> ManageClaimsPost(AppUserClaimsViewModel userClaimsViewModel, AppUser user);

    }
}
