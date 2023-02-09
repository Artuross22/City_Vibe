using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Net;

namespace City_Vibe.Models
{
    public class AppUser : IdentityUser
    {
        public string NickName { get; set; }
        [NotMapped]
        public string? RoleId { get; set; }
        [NotMapped]
        public string? Role { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
        public string? UserDescription { get; set; }
        public DateTime? Birthday { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }       

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Event> Event { get; set; }
        public ICollection<SaveClub> SaveClubs { get; set; }

        [NotMapped]
        public ICollection<LikeClub>? LikeClubs { get; set; }
        public ICollection<PostInfoInClub>? PostInfoInClub { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
