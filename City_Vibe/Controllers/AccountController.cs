using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AccountController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace City_Vibe.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISendGridEmail _sendGridEmail;
        private readonly IAccountService accountService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ISendGridEmail sendGridEmail,
            RoleManager<IdentityRole> roleManager , IAccountService _accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
             _sendGridEmail = sendGridEmail;
            _roleManager = roleManager;
            accountService = _accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string? returnurl = null)
        {
            returnurl = returnurl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }

                var request = await accountService.ExternalLoginConfirmation(model, info, returnurl);

                if (request)
                {
                   return LocalRedirect(returnurl);                 
                }

                ModelState.AddModelError("Email", "This user already exists");
            }
            ViewData["ReturnUrl"] = returnurl;
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            var request = await accountService.RegisterGet(returnUrl);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string? returnUrl = null)
        {
            registerViewModel.ReturnUrl = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {

                var request = await accountService.RegisterPost(registerViewModel, returnUrl);

                if (request)
                    return LocalRedirect(returnUrl);

                ModelState.AddModelError("Password", "User could not be created. Password not unique enough");
            }
            return View(registerViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnurl = null, string remoteError = null)
        {

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ReturnUrl"] = returnurl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginViewModel { Email = email });

            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnurl = null)
        {
            var redirecturl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnurl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return Challenge(properties, provider); 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                await _sendGridEmail.SendEmailAsync(model.Email, "Reset Email Confirmation", "Please reset email by going to this " +
                 "<a href=\"" + callbackurl + "\">link</a>");

                return RedirectToAction("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "User not found");
                    return View();
                }
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
            }
            return View(resetPasswordViewModel);
        }


        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
          
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.ReturnUrl = returnUrl ?? Url.Content("~/");
            return View(loginViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(loginViewModel);
                }
            }
            return View(loginViewModel);
        }

  

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

