using City_Vibe.Data;
using City_Vibe.Models;
using City_Vibe.ViewModels.RoleController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace City_Vibe.Controllers
{

    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RoleController(ApplicationDbContext db, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();
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
                //update
                var user = _db.Roles.FirstOrDefault(u => u.Id == id);
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
                var roleDb = _db.Roles.FirstOrDefault(u => u.Id == role.Id);
                if (roleDb == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                roleDb.Name = role.Name;
                roleDb.NormalizedName = role.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(roleDb);
            }
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var roleDb = _db.Roles.FirstOrDefault(u => u.Id == id);
            if (roleDb == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userRolesForThisRole = _db.UserRoles.Where(u => u.RoleId == id).Count();
            if (userRolesForThisRole > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            await _roleManager.DeleteAsync(roleDb);
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
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
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

