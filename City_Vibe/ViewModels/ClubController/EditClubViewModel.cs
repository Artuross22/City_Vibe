using City_Vibe.Models;
using System.ComponentModel.DataAnnotations;

namespace City_Vibe.ViewModels.ClubController
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
