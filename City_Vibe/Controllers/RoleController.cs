using City_Vibe.ViewModels.RoleController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using AutoMapper;

namespace City_Vibe.Controllers
{

    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWorkRepository;


        public RoleController( UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWorkRepos,
            IMapper mapp)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            unitOfWorkRepository = unitOfWorkRepos;
            mapper = mapp;
        }


        public IActionResult Index()
        {
            var roles = unitOfWorkRepository.RoleRepository.GetAll();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Upsert(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                var user = unitOfWorkRepository.RoleRepository.GetById(id);
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                return RedirectToAction(nameof(Index));
            }
            if (string.IsNullOrEmpty(role.Id))
            {
                //create
                await _roleManager.CreateAsync(new IdentityRole() { Name = role.Name });
            }
            else
            {
                //update
                var roleById = unitOfWorkRepository.RoleRepository.GetById(role.Id);
                if (roleById == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                roleById.Name = role.Name;
                roleById.NormalizedName = role.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(roleById);
            }
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var roleById = unitOfWorkRepository.RoleRepository.GetById(id);
            if (roleById == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userRolesForThisRole = unitOfWorkRepository.RoleRepository.UserRolesCount(id);

            if (userRolesForThisRole > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            await _roleManager.DeleteAsync(roleById);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

   

        public async Task<IActionResult> EditUserRole(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();

                var model = mapper.Map<ChangeRoleViewModel>(user);
                model.UserRoles = userRoles;
                model.AllRoles = allRoles;

                return RedirectToAction("Index");
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditUserRole(string userId, List<string> roles)
        {

            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}

