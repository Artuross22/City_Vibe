using City_Vibe.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.ViewModels.ClubController
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }

        public int CategoryId { get; set; }
        public Category? EventCategory { get; set; }

        public IEnumerable<SelectListItem>? EventList { get; set; }
        public string AppUserId { get; set; }
    }
}
