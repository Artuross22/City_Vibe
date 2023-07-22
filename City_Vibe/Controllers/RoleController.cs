using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using City_Vibe.Contracts;

namespace City_Vibe.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;

        public RoleController( IRoleService _roleService) => roleService = _roleService;

        public IActionResult Index() => View(roleService.Index());

        public IActionResult UserList() => View(roleService.UserList());


        [HttpGet]
        public IActionResult Upsert(string id)
        {
            if (String.IsNullOrEmpty(id)) return View();
            else return View(roleService.UpsertGet(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole role)
        {
            var request = await roleService.UpsertPost(role);

            if (request.Succeeded == false)
            {
                ModelState.AddModelError("Add and remove a role", "This role already exists");
                return View(role);
            }

            if(request.CurrentItem == false) return NotFound();  

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id)) return RedirectToAction(nameof(Index));

            var request = await roleService.Delete(id);
            if (request.CurrentItem == false) NotFound();

            if(request.Succeeded == false)
            {
                ModelState.AddModelError("Delete a role", "There are users associated with this role");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditUserRole(string userId)
        {
            if (String.IsNullOrEmpty(userId)) return RedirectToAction("/Account/Login");

            var request = await roleService.EditUserRoleGet(userId);
            if (request == null) return NotFound();
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(string userId, List<string> roles)
        {
            if (String.IsNullOrEmpty(userId)) return RedirectToAction("/Account/Login");

            var request = await roleService.EditUserRolePost(userId, roles);
            if(request.CurrentUser == false) return NotFound();
            return RedirectToAction("UserList");
        }
    }
}

