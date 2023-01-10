using City_Vibe.Models;

namespace City_Vibe.ViewModels.AppUserController
{
    public class EditProfileViewModel
    {
            public string? Id { get; set; }

            public string? ProfileImageUrl { get; set; }
            public string? City { get; set; }
            public string? Region { get; set; }
            public IFormFile? Image { get; set; }
           public Address Address { get; set; }
    }
}
