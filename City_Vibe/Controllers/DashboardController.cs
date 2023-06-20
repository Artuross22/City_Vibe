using City_Vibe.ViewModels.DashboardController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;


namespace City_Vibe.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IUnitOfWork unitOfWorkRepository;

        public DashboardController(IUnitOfWork unitOfWorkRespo)
        {
            unitOfWorkRepository = unitOfWorkRespo;
        }

        public async Task<IActionResult> Index()
        {
            var userEvents = await unitOfWorkRepository.DashboardRepository.GetAllUserEvent();
            var userClubs = await unitOfWorkRepository.DashboardRepository.GetAllUserClubs();
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
            var activeUsers = unitOfWorkRepository.AppUserRepository.GetAllUsersByIQueryable(userRoleName);

            IQueryable<Event> events =  unitOfWorkRepository.EventRepository.Find(x => x.Data >= DateTime.Now).Include(x => x.Category).OrderByDescending(x => x.Data);


            if (category != null && category != 0)
            {
                events = events.Where(p => p.CategoryId == category);
            }

            if (!string.IsNullOrEmpty(name))
            {
                activeUsers = activeUsers.Where(p => p.NickName.Contains(name) || p.UserDescription!.Contains(name));
            }

            List<Category> categories = await unitOfWorkRepository.CategoryRepository.GetAllAsync();
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
            var activeUsers = await unitOfWorkRepository.AppUserRepository.GetUsersByRole(userRoleName);

            var findActiveUsersByTime = await unitOfWorkRepository.EventRepository.Find(x => x.Data >= DateTime.Now).OrderByDescending(x => x.Data).ToListAsync();

          
            var dashboardViewModel = new ActiveActionsDasboardVM()
            {
                AppUsers = activeUsers,
                Events = findActiveUsersByTime,

            };

            return View(dashboardViewModel);
        }
    }
}

