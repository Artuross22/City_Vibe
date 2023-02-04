using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.AppUserController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using City_Vibe.Repository;
using System.Security.Claims;
using City_Vibe.Data;
using System.Linq;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data;

namespace City_Vibe.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUserController(IAppUserRepository userRepository, UserManager<AppUser> userManager, IPhotoService photoService,
            ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _photoService = photoService;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<AppUserViewModel> result = new List<AppUserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new AppUserViewModel()
                {
                    Id = user.Id,
                    City = user.City,
                    Region = user.Region,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg",
                };
                result.Add(userViewModel);
            }
            return View(result);
        }


    
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }
            var userDetailViewModel = new AppUserDetailViewModel()
            {
                Id = user.Id,
                City = user.City,
                Region = user.Region,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg",
            };
            return View(userDetailViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            var editMV = new EditProfileViewModel()
            {
                City = user.City,
                Region = user.Region,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(editMV);
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

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            if (editVM.Image != null) // only update profile image
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Failed to upload image");
                    return View("EditProfile", editVM);
                }

                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    _ = _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }

                user.ProfileImageUrl = photoResult.Url.ToString();
                editVM.ProfileImageUrl = user.ProfileImageUrl;


                await _userManager.UpdateAsync(user);
            }

            Address adress = new Address
            {
                City = editVM.Address.City,
                Region = editVM.Address.Region,
                ZipCode = editVM.Address.ZipCode,
                Street = editVM.Address.Street,
            };

            user.Address = adress;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "AppUser", new { user.Id });
        }



        [HttpGet]
        public async Task<IActionResult> ManageClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }



            var existingUserClaims = await _userManager.GetClaimsAsync(user);

            var model = new AppUserClaimsViewModel()
            {
                UserId = userId
            };
            foreach (Claim claim in ClaimStore.claimsList)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageClaims(AppUserClaimsViewModel userClaimsViewModel)
        {
            var user = await _userManager.FindByIdAsync(userClaimsViewModel.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                return View(userClaimsViewModel);
            }

            result = await _userManager.AddClaimsAsync(user,
                userClaimsViewModel.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.IsSelected.ToString())));

            if (!result.Succeeded)
            {
                return View(userClaimsViewModel);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

