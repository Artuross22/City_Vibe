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

            CreateMap<Club, DeleteClubViewModel>();
            CreateMap<DeleteClubViewModel, Club>();

            CreateMap<Club, EditClubViewModel>()
                .ForMember(x => x.URL, opt => opt.MapFrom(src => src.Image))
                .ForMember(x => x.Image, opt => opt.Ignore());


            CreateMap<EditClubViewModel, Club>()
                 .ForMember(x => x.Image, opt => opt.MapFrom(src => src.URL));




            //CreateMap<Event, EditEventViewModel>()
            //    .ForMember(x => x.URL, opt => opt.MapFrom(src => src.Image))
            //    .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Desciption))
            //    .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.Data))
            //    .ForMember(x => x.Image, opt => opt.Ignore());

        }
    }
}
