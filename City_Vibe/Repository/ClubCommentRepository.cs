using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class ClubCommentRepository : IClubCommentRepository
    {
        private readonly ApplicationDbContext contextDb;

        public ClubCommentRepository(ApplicationDbContext context)
        {
            contextDb = context;
        }

        public bool Add(CommentClub comment)
        {
            contextDb.Add(comment);
            return Save();
        }

        public bool Delete(CommentClub comment)
        {
            contextDb.Remove(comment);
            return Save();
        }

        public bool Update(CommentClub comment)
        {
            contextDb.Update(comment);
            return Save();
        }

        public bool AddReplyComment(ReplyCommentClub replyCommentClub)
        {
            contextDb.Add(replyCommentClub);
            return Save();
        }

        public ICollection<CommentClub> GetAllCommentClubInfoById(int id)
        {
            var commentsModel = contextDb.CommentClubs.Where(x => x.PostInfoInClubId == id).Include(x => x.ReplyCommentClubs).ThenInclude(x => x.AppUser).OrderByDescending(x => x.DateTime).ToList();
            return commentsModel;
        }

        public IEnumerable<CommentClub> GetAll()
        {
            return contextDb.CommentClubs.ToList();
        }

        public async Task<CommentClub> GetByIdAsync(int id)
        {
            return await contextDb.CommentClubs.Include(i => i.ReplyCommentClubs).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<CommentClub> GetByIdAsyncNoTracking(int id)
        {
            return await contextDb.CommentClubs.Include(i => i.ReplyCommentClubs).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }


        public bool Save()
        {
            var saved = contextDb.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
