using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Domain.Models
{
    public class ReplyCommentClub
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string? UserName { get; set; }

        public DateTime CreatedDate { get; set; }


        [ForeignKey("User")]
        public Guid? InternalUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [ForeignKey("CommentClub")]
        public int? CommentClubId { get; set; }
        public CommentClub? CommentClub { get; set; }
    }
}
