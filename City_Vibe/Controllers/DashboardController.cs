using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.DashboardController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace City_Vibe.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository dashboardRespository;
        private readonly IPhotoService photoService;
        private readonly IAppUserRepository appUserRepository;
        private readonly IEventRepository eventRepository;
        private readonly ApplicationDbContext dbContext;

        public DashboardController(IDashboardRepository dashboardRespo, IPhotoService photoServ, IAppUserRepository appUserRepos, IEventRepository eventRepo,
            ApplicationDbContext dbContex)
        {
            dashboardRespository = dashboardRespo;
            photoService = photoServ;
            appUserRepository = appUserRepos;
            eventRepository = eventRepo;
            dbContext = dbContex;
        }

        public async Task<IActionResult> Index()
        {
            var userEvents = await dashboardRespository.GetAllUserEvent();
            var userClubs = await dashboardRespository.GetAllUserClubs();
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
            var activeUsers =  appUserRepository.GetAllUsersByIQueryable(userRoleName);

            

            IQueryable<Event> events =  eventRepository.ActiveEventBytimeIQueryable();
            
         
            if (category != null && category != 0)
            {
                events = events.Where(p => p.CategoryId == category);
            }

            if (!string.IsNullOrEmpty(name))
            {
                activeUsers = activeUsers.Where(p => p.NickName.Contains(name) || p.UserDescription!.Contains(name));
            }

            List<Category> categories = dbContext.Categories.ToList();
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
            var activeUsers = await appUserRepository.GetUsersByRole(userRoleName);

            var userEvents = await eventRepository.ActiveEventBytime();

            var dashboardViewModel = new ActiveActionsDasboardVM()
            {
                AppUsers = activeUsers,
                Events = userEvents,

            };

            return View(dashboardViewModel);
        }
    }
}

