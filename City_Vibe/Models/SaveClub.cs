using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Models
{
    public class SaveClub
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }



        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public ICollection<Club> Clubs { get; set; }

    }
}
