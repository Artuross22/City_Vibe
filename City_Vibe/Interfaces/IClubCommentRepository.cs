using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IClubCommentRepository : IGenericRepository<CommentClub>
    {

        ICollection<CommentClub> GetAllCommentsClubById(int id);

        bool AddReplyComment(ReplyCommentClub replyComment);

        bool Save();
    }
}
