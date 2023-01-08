using City_Vibe.Interfaces;
using City_Vibe.ViewModels.DashboardController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
            private readonly IDashboardRepository dashboardRespository;
            private readonly IPhotoService photoService;

            public DashboardController(IDashboardRepository dashboardRespo, IPhotoService photoServ)
            {
                dashboardRespository = dashboardRespo;
                photoService = photoServ;
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
        }


    }

