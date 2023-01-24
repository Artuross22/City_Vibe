using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Desciption { get; set; }

        public DateTime? Data { get; set; }

        public string Image { get; set; }

        public string? City { get; set; }

        public Category? Category { get; set; }

        public int CategoryId { get; set; }


        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }



        [ForeignKey("Replies")]
        public int? RepliesId { get; set; }
        public ReplyComment? Replies { get; set; }
        public ICollection<Comment>? Comments { get; set; }

        public int? SaveEventId { get; set; }
        public SaveEvent? SaveEvent { get; set; }


        [ForeignKey("Club")]
        public int? ClubId { get; set; }
        public Club? Club { get; set; }
    }
}
