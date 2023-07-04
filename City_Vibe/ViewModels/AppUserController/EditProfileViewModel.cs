

using City_Vibe.Domain.Models;

namespace City_Vibe.ViewModels.AppUserController
{
    public class EditProfileViewModel
    {
            public string? Id { get; set; }
            public string? ProfileImageUrl { get; set; }
            public string? City { get; set; }
            public string? Region { get; set; }
            public IFormFile? Image { get; set; }
            public Address? Address { get; set; }
            public int? AddressId { get; set; }
            public bool? Succeeded { get; set; }
            public bool? ErrorPhoto { get; set; }
    }
}
