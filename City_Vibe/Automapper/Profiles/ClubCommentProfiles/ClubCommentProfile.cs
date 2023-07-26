using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.ClubCommentController;

namespace City_Vibe.Automapper.Profiles.ClubCommentProfiles
{
    public class ClubCommentProfile : Profile
    {
        public ClubCommentProfile()
        {
            CreateMap<PostCommentClubViewModel, CommentClub>()
                 .ForMember(x => x.Id, opt => opt.Ignore())
                   .ForMember(x => x.UserName, opt => opt.Ignore())
                    .ForMember(x => x.ForeignUserId, opt => opt.Ignore())
                      .ForMember(x => x.AppUser, opt => opt.Ignore())
                        .ForMember(x => x.PostInfoInClub, opt => opt.Ignore())
                          .ForMember(x => x.ReplyCommentClubs, opt => opt.Ignore())
                            .ForMember(x => x.DateTime, opt => opt.Ignore());

           
            CreateMap<ReplyCommentClubViewModel, ReplyCommentClub>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                   .ForMember(x => x.UserName, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                      .ForMember(x => x.InternalUserId, opt => opt.Ignore())
                        .ForMember(x => x.AppUser, opt => opt.Ignore())
                          .ForMember(x => x.CommentClub, opt => opt.Ignore());
        }
    }
}
