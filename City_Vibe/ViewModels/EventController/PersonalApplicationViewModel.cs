using City_Vibe.Models;
using Microsoft.VisualBasic;

namespace City_Vibe.ViewModels.EventController
{
    public class PersonalApplicationViewModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime? StartEvent { get; set; }
        public string? UserName { get; set; }
        public string? Category { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Email { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateStatement { get; set; }

        public string Phone { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? EventId { get; set; }
    }
}
