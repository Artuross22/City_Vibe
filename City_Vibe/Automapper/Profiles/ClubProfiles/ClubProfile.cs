using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.ClubController;
using Microsoft.Extensions.Logging;

namespace City_Vibe.Automapper.Profiles.ClubProfiles
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            CreateMap<Club, DetailClubViewModel>()
                 .ForMember(x => x.SaveClubId, opt => opt.Ignore())
                     .ForMember(x => x.SaveClub, opt => opt.Ignore())
                         .ForMember(x => x.PostInfoInClubs, opt => opt.Ignore())
                                 .ForMember(x => x.CountLikes, opt => opt.Ignore())
                                    .ForMember(x => x.SaveClubs, opt => opt.Ignore())
                                      .ForMember(x => x.Succeeded, opt => opt.Ignore());
                                    


            CreateMap<CreateClubViewModel, Club>()
                 .ForMember(x => x.Category, opt => opt.Ignore())
                     .ForMember(x => x.EventId, opt => opt.Ignore())
                         .ForMember(x => x.Event, opt => opt.Ignore())
                                 .ForMember(x => x.Events, opt => opt.Ignore())
                                      .ForMember(x => x.PostInfoInClub, opt => opt.Ignore())
                                           .ForMember(x => x.LikeClubs, opt => opt.Ignore())
                                          .ForMember(x => x.AppUser, opt => opt.Ignore());


            CreateMap<PostInformationClubViewModel, PostInfoInClub>()
                      .ForMember(x => x.Club, opt => opt.Ignore())
                                          .ForMember(x => x.AppUser, opt => opt.Ignore());

           
            CreateMap<PostInfoInClub, PostInformationDetailViewModel>()
                   .ForMember(x => x.CommentClub, opt => opt.Ignore());


            CreateMap<Club, DeleteClubViewModel>();
            

            CreateMap<DeleteClubViewModel, Club>();

            CreateMap<Club, EditClubViewModel>()
                .ForMember(x => x.URL, opt => opt.MapFrom(src => src.Image))
                .ForMember(x => x.Image, opt => opt.Ignore())
                    .ForMember(x => x.Succeeded, opt => opt.Ignore());
            


            CreateMap<EditClubViewModel, Club>()
                 .ForMember(x => x.Image, opt => opt.MapFrom(src => src.URL))
                   .ForMember(x => x.EventId, opt => opt.Ignore())
                     .ForMember(x => x.Event, opt => opt.Ignore())
                         .ForMember(x => x.Events, opt => opt.Ignore())
                                 .ForMember(x => x.LikeClubs, opt => opt.Ignore())
                                      .ForMember(x => x.PostInfoInClub, opt => opt.Ignore())
                                          .ForMember(x => x.AppUser, opt => opt.Ignore());
        }
    }
}
