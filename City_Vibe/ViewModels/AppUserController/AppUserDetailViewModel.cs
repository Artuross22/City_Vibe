using City_Vibe.Models;

namespace City_Vibe.ViewModels.AppUserController
{
    public class AppUserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string ProfileImageUrl { get; set; }
        public Address? Address { get; set; }


        public string Location => (City, Region) switch
        {
            (string city, string state) => $"{city}, {state}",
            (string city, null) => city,
            (null, string state) => state,
            (null, null) => "",
        };
    }
}
