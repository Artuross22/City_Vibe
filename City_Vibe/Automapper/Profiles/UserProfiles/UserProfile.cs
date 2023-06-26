using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.RoleController;

namespace City_Vibe.Automapper.Profiles.UserProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, ChangeRoleViewModel>();
        }
    }
}
