using City_Vibe.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.ViewModels.ClubController
{
    public class PostInformationClubViewModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string PostInformation { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime DateAndTime { get; set; }
        public int ClubId { get; set; }
        public string AppUserId { get; set; }

    }
}
