namespace City_Vibe.ViewModels.AppUserController
{
    public class AppUserViewModel
    {

        public string Id { get; set; }
        public string UserName { get; set; }

        public string? City { get; set; }
        public string? Region { get; set; }
        public string ProfileImageUrl { get; set; }

        public string Location => (City, Region) switch
        {
            (string city, string region) => $"{city}, {region}",
            (string city, null) => city,
            (null, string state) => state,
            (null, null) => "",
        };
    }
}
