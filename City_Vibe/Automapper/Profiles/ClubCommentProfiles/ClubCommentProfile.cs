using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.ClubCommentController;

namespace City_Vibe.Automapper.Profiles.ClubCommentProfiles
{
    public class ClubCommentProfile : Profile
    {
        public ClubCommentProfile()
        {

            CreateMap<PostCommentClubViewModel, CommentClub>();
            CreateMap<ReplyCommentClubViewModel, ReplyCommentClub>();
        }
    }
}
