using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.Models
{
    public class Club
    {

        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }


        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }


        [ForeignKey("Event")]
        public int? EventId { get; set; }
        public Event? Event { get; set; }

        [NotMapped]
        public ICollection<Event>? Events { get; set; }
        public ICollection<LikeClub>? LikeClubs { get; set; }

        public ICollection<PostInfoInClub>? PostInfoInClub{ get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
