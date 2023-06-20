
using City_Vibe.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_Vibe.ViewModels.ClubController
{
    public class PostInformationDetailViewModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string PostInformation { get; set; }
        public string? Image { get; set; }
        public DateTime DateAndTime { get; set; }

        public ICollection<CommentClub>? CommentClub { get; set; }
    }
}
