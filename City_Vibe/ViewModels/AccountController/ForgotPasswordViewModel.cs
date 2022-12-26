using System.ComponentModel.DataAnnotations;

namespace City_Vibe.ViewModels.AccountController
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
