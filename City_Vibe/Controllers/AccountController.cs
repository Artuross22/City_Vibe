using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.AccountController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISendGridEmail _sendGridEmail;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ISendGridEmail sendGridEmail, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
             _sendGridEmail = sendGridEmail;
            _roleManager = roleManager;
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
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            if (!await _roleManager.RoleExistsAsync("Admin")) 
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Trainer"));
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
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string? returnUrl = null)
        {
            registerViewModel.ReturnUrl = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new AppUser { Email = registerViewModel.Email, UserName = registerViewModel.UserName, NickName = registerViewModel.UserName };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    if (registerViewModel.RoleSelected != null && registerViewModel.RoleSelected.Length > 0 && registerViewModel.RoleSelected == "Trainer")
                    {
                        await _userManager.AddToRoleAsync(user, "Trainer");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                ModelState.AddModelError("Password", "User could not be created. Password not unique enough");
            }
            return View(registerViewModel);
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
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

