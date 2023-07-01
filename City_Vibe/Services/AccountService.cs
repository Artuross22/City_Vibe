using AutoMapper;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AccountController;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;


        public AccountService(
            UserManager<AppUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            SignInManager<AppUser> _signInManager)
        {

            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;

        }

        public async Task<bool> ExternalLoginConfirmation(ExternalLoginViewModel model, ExternalLoginInfo info , string? returnurl = null)
        {

            var user = new AppUser { UserName = model.Email, Email = model.Email, NickName = model.Name };
            var result = await userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                result = await userManager.AddLoginAsync(user, info);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    await signInManager.UpdateExternalAuthenticationTokensAsync(info);
                }
            }

            return result.Succeeded;      
        }

        public async Task<RegisterViewModel> RegisterGet(string? returnUrl = null)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Trainer"));
            }

            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Trainer",
                Text = "Trainer"
            });

            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });

            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.RoleList = listItems;
            registerViewModel.ReturnUrl = returnUrl;

            return registerViewModel;
        }

        public async Task<bool> RegisterPost(RegisterViewModel registerViewModel, string? returnUrl = null)
        {
            var user = new AppUser { Email = registerViewModel.Email, UserName = registerViewModel.UserName, NickName = registerViewModel.UserName };
            var result = await userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                if (registerViewModel.RoleSelected != null && registerViewModel.RoleSelected.Length > 0 && registerViewModel.RoleSelected == "Trainer")
                {
                    await userManager.AddToRoleAsync(user, "Trainer");
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                await signInManager.SignInAsync(user, isPersistent: false);

                return result.Succeeded;
            }
            return false;
        }



        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model, AppUser user)
        {
            throw new NotImplementedException();
        }


        public IActionResult ExternalLogin(string provider, string returnurl = null)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ExternalLoginCallback(string returnurl = null, string remoteError = null)
        {
            throw new NotImplementedException();
        }

     

        public IActionResult Login(string? returnUrl = null)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            throw new NotImplementedException();
        }

    

        public IActionResult ResetPassword(string code = null)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            throw new NotImplementedException();

        }
    }
}
