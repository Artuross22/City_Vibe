using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string? Body { get; set; }

        public string? UserName { get; set; }


        [ForeignKey("User")]
        public Guid ForeignUserId { get; set; }


        public AppUser? AppUser { get; set; }


        public DateTime DateTime { get; set; }

        public int? EventId { get; set; }

        public Event? Event { get; set; }


        public IList<ReplyComment> ReplyComment { get; set; }

        public Comment()
        {
            ReplyComment = new List<ReplyComment>();
        }
    }
}
