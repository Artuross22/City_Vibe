using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.DashboardController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IUnitOfWork unitOfWorkRespository;
        public IDashboardRepository dashboardRepository;

        public DashboardController(IUnitOfWork unitOfWorkRespo, IDashboardRepository dashboardRepos)
        {
            unitOfWorkRespository = unitOfWorkRespo;
            dashboardRepository = dashboardRepos;
        }

        public async Task<IActionResult> Index()
        {
            var userEvents = await dashboardRepository.GetAllUserEvent();
            var userClubs = await dashboardRepository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Events = userEvents,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }

        public async Task<ActionResult> ActiveUsersIndex(int? category, string? name)
        {

            var userRoleName = UserRoles.ActiveUser;
            var activeUsers = unitOfWorkRespository.AppUserRepository.GetAllUsersByIQueryable(userRoleName);

            IQueryable<Event> events = unitOfWorkRespository.EventRepository.ActiveEventBytimeIQueryable();
            
         
            if (category != null && category != 0)
            {
                events = events.Where(p => p.CategoryId == category);
            }

            if (!string.IsNullOrEmpty(name))
            {
                activeUsers = activeUsers.Where(p => p.NickName.Contains(name) || p.UserDescription!.Contains(name));
            }

            List<Category> categories = await unitOfWorkRespository.CategoryRepository.GetAllAsync();
            categories.Insert(0, new Category { Name = "All", Id = 0 });

            UserListViewModel viewModel = new UserListViewModel
            {
                Users = activeUsers.ToList(),
                Events = events.ToList(),
                Category = new SelectList(categories, "Id", "Name", category),              
                Name = name
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ActiveUsers()
        {

            var userRoleName = UserRoles.ActiveUser;
            var activeUsers = await unitOfWorkRespository.AppUserRepository.GetUsersByRole(userRoleName);

            var userEvents = await unitOfWorkRespository.EventRepository.ActiveEventBytime();

            var dashboardViewModel = new ActiveActionsDasboardVM()
            {
                AppUsers = activeUsers,
                Events = userEvents,

            };

            return View(dashboardViewModel);
        }
    }
}

