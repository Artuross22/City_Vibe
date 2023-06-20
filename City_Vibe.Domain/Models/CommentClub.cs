using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Domain.Models
{
    public class CommentClub
    {
        public int Id { get; set; }

        public string? Body { get; set; }

        public string? UserName { get; set; }


        [ForeignKey("User")]
        public Guid ForeignUserId { get; set; }


        public AppUser? AppUser { get; set; }


        public DateTime DateTime { get; set; }


        [ForeignKey("PostInfoInClub")]
        public int? PostInfoInClubId { get; set; }

        public PostInfoInClub? PostInfoInClub { get; set; }


        public IList<ReplyCommentClub> ReplyCommentClubs { get; set; }

        public CommentClub()
        {
            ReplyCommentClubs = new List<ReplyCommentClub>();
        }
    }
}
