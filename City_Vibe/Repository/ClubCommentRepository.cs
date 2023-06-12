using City_Vibe.Data;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Repository
{
    public class ClubCommentRepository : GenericRepository<CommentClub>, IClubCommentRepository
    {
        private readonly ApplicationDbContext contextDb;

        public ClubCommentRepository(ApplicationDbContext context) : base(context) => contextDb = context;
       
        public ICollection<CommentClub> GetAllCommentsClubById(int id)
        {
            return contextDb.CommentClubs.Where(x => x.PostInfoInClubId == id).Include(x => x.ReplyCommentClubs).ThenInclude(x => x.AppUser).OrderByDescending(x => x.DateTime).ToList();          
        }

        public bool AddReplyComment(ReplyCommentClub replyCommentClub)
        {
            contextDb.Add(replyCommentClub);
            return Save();
        }

        public bool Save()
        {
            var saved = contextDb.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
