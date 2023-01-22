using City_Vibe.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.ViewModels.ClubController
{
    public class DetailClubViewModel
    {
     
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

     

        public int? EventId { get; set; }
        public Event? Event { get; set; }
        public ICollection<Event>? Events { get; set; }


        public int? SaveClubId { get; set; }
        public SaveClub? SaveClub { get; set; }
        public ICollection<SaveClub>? SaveClubs { get; set; }



        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}

