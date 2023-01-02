using City_Vibe.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.ViewModels.EventController
{
    public class EventDetailViewModel
    {
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


        public ICollection<Comment>? Comments { get; set; }

        public EventDetailViewModel()
        {
            Comments = new List<Comment>();
        }
    }
}
