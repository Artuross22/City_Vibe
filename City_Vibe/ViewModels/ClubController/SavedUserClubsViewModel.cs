

using City_Vibe.Domain.Models;

namespace City_Vibe.ViewModels.ClubController
{
    public class SavedUserClubsViewModel
    {
        public string? Name { get; set; }
        public ICollection<Club>? Clubs { get; set; }

        public IEnumerable<AppUser> Users { get; set; } = new List<AppUser>();
    }
}
