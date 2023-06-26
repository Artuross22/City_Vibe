using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AppUserController;

namespace City_Vibe.Automapper.Profiles.AppUserProfile
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserDetailViewModel>();

            CreateMap<AppUser, EditProfileViewModel>();

            CreateMap<AppUser, AppUserViewModel>();
        }

    }
}
