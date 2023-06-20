using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Domain.Models
{
    public class PostInfoInClub
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string PostInformation { get;set; }
        public string? Image { get; set; }
        public DateTime DateAndTime { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public Club? Club { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; } 
    }
}
