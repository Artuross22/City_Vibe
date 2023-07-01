using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AccountController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Contracts
{
    public interface IAccountService
    {
        Task<bool> ExternalLoginConfirmation(ExternalLoginViewModel model, ExternalLoginInfo info, string? returnurl = null);

        Task<RegisterViewModel> RegisterGet(string? returnUrl = null);

        Task<bool> RegisterPost(RegisterViewModel registerViewModel, string? returnUrl = null);


        Task<IActionResult> ExternalLoginCallback(string returnurl = null, string remoteError = null);

        IActionResult ExternalLogin(string provider, string returnurl = null);

        Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model,AppUser user);

        IActionResult ResetPassword(string code = null);

        Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);

        IActionResult Login(string? returnUrl = null);

        Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl);

    }
}
