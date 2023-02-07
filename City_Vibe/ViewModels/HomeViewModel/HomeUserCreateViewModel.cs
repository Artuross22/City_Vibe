using System.ComponentModel.DataAnnotations;

namespace City_Vibe.ViewModels.HomeViewModel
{
    public class HomeUserCreateViewModel
    {
        public string? UserName { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
