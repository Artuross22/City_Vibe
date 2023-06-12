using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.AppUserController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Data;

namespace City_Vibe.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IUnitOfWork unitOfWorkRepository;
        public  readonly  IHttpContextAccessor сontextAccessor;
        private readonly IPhotoService photoService;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
      
        

        public AppUserController(
            UserManager<AppUser> _userManager,
            IPhotoService _photoService,
            RoleManager<IdentityRole> _roleManager,
            IHttpContextAccessor _сontextAccessor,
            IUnitOfWork _unitOfWorkRepository
            )
        {
            userManager = _userManager;
            photoService = _photoService;
            roleManager = _roleManager;
            сontextAccessor = _сontextAccessor;
            unitOfWorkRepository = _unitOfWorkRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {

            var users = await unitOfWorkRepository.AppUserRepository.GetAllAsync();
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
            var returnUser = await unitOfWorkRepository.AppUserRepository.GetUserByIdIncludeAdress(id);
            if (returnUser == null)
            {
                return RedirectToAction("Index", "Users");
            }
            var userDetailViewModel = new AppUserDetailViewModel()
            {
                Id = returnUser.Id,
                City = returnUser.City,
                Region = returnUser.Region,
                UserName = returnUser.UserName,
                Address = returnUser.Address,
                ProfileImageUrl = returnUser.ProfileImageUrl ?? "/img/avatar-male-4.jpg",
            };
            return View(userDetailViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await userManager.GetUserAsync(User);

            var returnUser = await unitOfWorkRepository.AppUserRepository.GetUserByIdIncludeAdress(user.Id);

            if (returnUser == null)
            {
                return View("Error");
            }

            var editMV = new EditProfileViewModel()
            {
                City = returnUser.City,
                Region = returnUser.Region,
                ProfileImageUrl = returnUser.ProfileImageUrl,
                AddressId = returnUser.AddressId,
                Address = returnUser.Address,
         
                
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

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            if (editVM.Image != null) 
            {
                var photoResult = await photoService.AddPhotoAsync(editVM.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Failed to upload image");
                    return View("EditProfile", editVM);
                }

                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    _ = photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }

                user.ProfileImageUrl = photoResult.Url.ToString();
                await userManager.UpdateAsync(user);
            }

            user.Address = editVM.Address;

            await userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "AppUser", new { user.Id });
        }



        [HttpGet]
        public async Task<IActionResult> ManageClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }



            var existingUserClaims = await userManager.GetClaimsAsync(user);

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
            var user = await userManager.FindByIdAsync(userClaimsViewModel.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                return View(userClaimsViewModel);
            }

            result = await userManager.AddClaimsAsync(user,
                userClaimsViewModel.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.IsSelected.ToString())));

            if (!result.Succeeded)
            {
                return View(userClaimsViewModel);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

