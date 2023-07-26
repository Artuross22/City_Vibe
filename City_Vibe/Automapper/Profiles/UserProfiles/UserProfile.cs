using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.RoleController;

namespace City_Vibe.Automapper.Profiles.UserProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, ChangeRoleViewModel>()
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.UserEmail, opt => opt.Ignore())
                  .ForMember(x => x.AllRoles, opt => opt.Ignore())
              .ForMember(x => x.UserRoles, opt => opt.Ignore());
        }
    }
}
