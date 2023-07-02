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


    }
}
