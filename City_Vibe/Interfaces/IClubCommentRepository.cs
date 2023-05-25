using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IClubCommentRepository : IGenericRepository<CommentClub>
    {
        public IEnumerable<CommentClub> GetAll();
        public Task<CommentClub> GetByIdAsync(int id);
        Task<CommentClub> GetByIdAsyncNoTracking(int id);
        ICollection<CommentClub> GetAllCommentClubInfoById(int id);
        bool AddReplyComment(ReplyCommentClub replyComment);
        bool Save();
    }
}
