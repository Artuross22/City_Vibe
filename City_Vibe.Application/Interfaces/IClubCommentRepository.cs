
using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface IClubCommentRepository : IGenericRepository<CommentClub>
    {

        ICollection<CommentClub> GetAllCommentsClubById(int id);

        bool AddReplyComment(ReplyCommentClub replyComment);

        bool Save();
    }
}
