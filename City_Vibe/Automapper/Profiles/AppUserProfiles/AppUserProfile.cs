using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AppUserController;

namespace City_Vibe.Automapper.Profiles.AppUserProfile
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserDetailViewModel>()
                .ForMember(x => x.Succeeded, opt => opt.Ignore());

            CreateMap<AppUser, EditProfileViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ForMember(x => x.Succeeded, opt => opt.Ignore())
                    .ForMember(x => x.ErrorPhoto, opt => opt.Ignore());

            CreateMap<AppUser, AppUserViewModel>();
        }

    }
}
