using City_Vibe.ViewModels.AppUserController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Data;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using AutoMapper;
using City_Vibe.Contracts;
using City_Vibe.Infrastructure.ExtensionMethod;

namespace City_Vibe.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserService appUserService;
        private readonly UserManager<AppUser> userManager;

        public AppUserController(
        UserManager<AppUser> _userManager,
        IAppUserService _appUserService)
        {
            userManager = _userManager;
            appUserService = _appUserService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var result = await appUserService.Index();
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var result = await appUserService.Detail(id);
            if (result.Succeeded == false) return RedirectToAction("Index", "Users");
            else return View(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await userManager.GetUserAsync(User);
            var result = await appUserService.EditProfileGet(user);

            if (result.Succeeded == false) return View("Error");
            return View(result);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditProfile", editVM);
            }

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            var requeest = await appUserService.EditProfilePost(editVM, user);

            if (requeest.ErrorPhoto == true)
            {
                ModelState.AddModelError("Image", "Failed to upload image");
                return View("EditProfile", editVM);
            }

            return RedirectToAction("Detail", "AppUser", new { user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> ManageClaims()
        {
            var userId = await userManager.GetUserAsync(User);

            var user = await userManager.FindByIdAsync(userId.Id);

            if (user == null)
            {
                return NotFound();
            }

            var request = await appUserService.ManageClaimsGet(user);
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageClaims(AppUserClaimsViewModel userClaimsViewModel)
        {
            var user = await userManager.FindByIdAsync(userClaimsViewModel.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var request = await appUserService.ManageClaimsPost(userClaimsViewModel, user);

            if (!request.Succeeded)
            {
                return View(userClaimsViewModel);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

