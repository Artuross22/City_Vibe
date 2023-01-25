using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Models
{
    public class LikeClub
    {
        public int Id { get; set; }
        public int Like { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }


        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public Club? Club { get; set; }
    }
}
