using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IClubCommentRepository
    {
        public IEnumerable<CommentClub> GetAll();
        public Task<CommentClub> GetByIdAsync(int id);
        Task<CommentClub> GetByIdAsyncNoTracking(int id);
        ICollection<CommentClub> GetAllCommentClubInfoById(int id);

        bool AddReplyComment(ReplyCommentClub replyComment);
        bool Add(CommentClub commentClub);
        bool Update(CommentClub commentClub);
        bool Delete(CommentClub commentClub);
        bool Save();
    }
}
