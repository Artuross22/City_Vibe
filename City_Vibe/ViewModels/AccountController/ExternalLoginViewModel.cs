using System.ComponentModel.DataAnnotations;

namespace City_Vibe.ViewModels.AccountController
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
