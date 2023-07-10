using AutoMapper;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.Services.Base;
using City_Vibe.ViewModels.RoleController;
using Microsoft.AspNetCore.Identity;

namespace City_Vibe.Services
{
    public class RoleService : IRoleService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWorkRepository;


        public RoleService(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWorkRepos,
            IMapper mapp)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            unitOfWorkRepository = unitOfWorkRepos;
            mapper = mapp;
        }


        public IEnumerable<IdentityRole> Index() => unitOfWorkRepository.RoleRepository.GetAll();

        public IdentityRole UpsertGet(string id) => unitOfWorkRepository.RoleRepository.GetById(id);

        public IEnumerable<AppUser> UserList() => _userManager.Users.ToList();


        public async Task<Response> UpsertPost(IdentityRole role)
        {
            Response response = new Response(); 

            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                response.Succeeded = false;
                return response;
            }

            if (string.IsNullOrEmpty(role.Id))
            {
                //create
                await _roleManager.CreateAsync(new IdentityRole() { Name = role.Name });
            }
            else
            {
                //update
                var curRoleById = unitOfWorkRepository.RoleRepository.GetById(role.Id);
                if (curRoleById == null)
                {
                    response.CurrentItem = false;
                    return response;
                }
                curRoleById.Name = role.Name;
                curRoleById.NormalizedName = role.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(curRoleById);
            }

            response.Succeeded = true;
            return response;
        }

        public async Task<Response> Delete(string id)
        {
            Response response = new Response();
            var roleById = unitOfWorkRepository.RoleRepository.GetById(id);
            if (roleById == null)
            {
                response.CurrentItem = false;
                return response;
            }

            var userRolesForThisRole = unitOfWorkRepository.RoleRepository.UserRolesCount(id);

            if (userRolesForThisRole > 0)
            {
                response.Succeeded = false;
                return response;
            }
            await _roleManager.DeleteAsync(roleById);
            response.Succeeded = true;
            return response;
        }

        public async Task<ChangeRoleViewModel> EditUserRoleGet(string userId)
        {
            var model = new ChangeRoleViewModel();

            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();


                model = mapper.Map<ChangeRoleViewModel>(user);
                model.UserRoles = userRoles;
                model.AllRoles = allRoles;
                model.UserId = userId;
                return model;
            }

            return model;         
        }

        public async Task<Response> EditUserRolePost(string userId, List<string> roles)
        {
            Response response = new Response();

            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                response.Succeeded = true;
                return response;
            }
            response.CurrentUser = false;
            return response;
        }

    }
}
