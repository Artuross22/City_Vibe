using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.ClubController;

namespace City_Vibe.Automapper.Profiles.ClubProfiles
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            CreateMap<Club, DetailClubViewModel>();
            CreateMap<CreateClubViewModel, Club>();

            CreateMap<PostInformationClubViewModel, PostInfoInClub>();
            CreateMap<PostInfoInClub, PostInformationDetailViewModel>();
        }
    }
}
