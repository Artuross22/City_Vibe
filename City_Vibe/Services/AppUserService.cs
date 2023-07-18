using AutoMapper;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.AppUserController;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace City_Vibe.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWorkRepository;
        public readonly IHttpContextAccessor сontextAccessor;
        private readonly IPhotoService photoService;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AppUserService(
        UserManager<AppUser> _userManager,
        IPhotoService _photoService,
        RoleManager<IdentityRole> _roleManager,
        IHttpContextAccessor _сontextAccessor,
        IUnitOfWork _unitOfWorkRepository,
        IMapper _mapper
        )
        {
            userManager = _userManager;
            photoService = _photoService;
            roleManager = _roleManager;
            сontextAccessor = _сontextAccessor;
            unitOfWorkRepository = _unitOfWorkRepository;
            mapper = _mapper;
        }

        public async Task<IEnumerable<AppUserViewModel>> Index()
        {
            var users = await unitOfWorkRepository.AppUserRepository.GetAllAsync();
            List<AppUserViewModel> result = new List<AppUserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = mapper.Map<AppUserViewModel>(user);

                userViewModel.ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg";
                result.Add(userViewModel);
            }
            return result;

        }

        public async Task<AppUserDetailViewModel> Detail(string id)
        {
            var userDetailViewModel = new AppUserDetailViewModel();

            var returnUser = await unitOfWorkRepository.AppUserRepository.GetUserByIdIncludeAdress(id);
            if (returnUser == null)
            {
                userDetailViewModel.Succeeded = false;
                return userDetailViewModel;
            }

             userDetailViewModel = mapper.Map<AppUserDetailViewModel>(returnUser);

            userDetailViewModel.ProfileImageUrl = returnUser.ProfileImageUrl ?? "/img/avatar-male-4.jpg";
            return userDetailViewModel;
        }

        public async Task<EditProfileViewModel> EditProfileGet(AppUser curUser)
        {
            var editProfileViewModel = new EditProfileViewModel();  
            var returnUser = await unitOfWorkRepository.AppUserRepository.GetUserByIdIncludeAdress(curUser.Id);

            if (returnUser == null)
            {
                editProfileViewModel.Succeeded = false;
                return editProfileViewModel;
            }

            editProfileViewModel = mapper.Map<EditProfileViewModel>(returnUser);

            return editProfileViewModel;
        }

        public async Task<EditProfileViewModel> EditProfilePost(EditProfileViewModel editVM, AppUser user)
        {

            if (editVM.Image != null)
            {
                var photoResult = await photoService.AddPhotoAsync(editVM.Image);

                if (photoResult.Error != null)
                {
                   
                    editVM.ErrorPhoto = true;
                    return editVM;
                }

                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    _ = photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }

                user.ProfileImageUrl = photoResult.Url.ToString();
                await userManager.UpdateAsync(user);
            }

            user.Address = editVM.Address;
            var result = await userManager.UpdateAsync(user);

            if (result != null) editVM.Succeeded = true;
            return editVM;
        }


        public async Task<AppUserClaimsViewModel> ManageClaimsGet(AppUser user)
        {

            var existingUserClaims = await userManager.GetClaimsAsync(user);
            var model = new AppUserClaimsViewModel()
            {
                UserId = user.Id
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

            return model;
        }

        public async Task<AppUserClaimsViewModel> ManageClaimsPost(AppUserClaimsViewModel userClaimsViewModel, AppUser user)
        {
         
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                return userClaimsViewModel;
            }

            result = await userManager.AddClaimsAsync(user,
                userClaimsViewModel.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.IsSelected.ToString())));

            if (!result.Succeeded)
            {
                return userClaimsViewModel;
            }

            userClaimsViewModel.Succeeded = true;
            return userClaimsViewModel;

        }
    }
}
