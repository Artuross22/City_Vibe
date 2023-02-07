using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Helpers;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.ViewModels.AccountController;
using City_Vibe.ViewModels.HomeViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace City_Vibe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        private readonly IClubRepository clubRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILocationService _locationService;
        private readonly IHttpContextAccessor сontextAccsess;

        public HomeController(IClubRepository clubRepositoryAccess, SignInManager<AppUser> signInManagerAccess, UserManager<AppUser> userManagerAccess, ILogger<HomeController> _logger, IHttpContextAccessor сontextAccs)
        {
            signInManager = signInManagerAccess;
            userManager = userManagerAccess;
            clubRepository = clubRepositoryAccess;
            logger = _logger;
            сontextAccsess = сontextAccs;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();
            try
            {

                string url = "https://ipinfo.io?token=7c545833f3a6f8";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null)
                {
                    homeViewModel.Clubs = await clubRepository.GetClubByCity(homeViewModel.City);
                }
                return View(homeViewModel);
            }
            catch (Exception)
            {
                homeViewModel.Clubs = null;
            }

            return View(homeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel homeVM)
        {
            var curUserId = сontextAccsess.HttpContext.User.GetUserId();

            if (!ModelState.IsValid) return View(homeVM);

            var createVM = homeVM.Register;
            var user = await userManager.FindByEmailAsync(createVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("Register.Email", "This email is not registered");
                return View(homeVM);
            }

            if(user.Id == curUserId)
            {
                await userManager.AddToRoleAsync(user, UserRoles.ActiveUser);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Register.Email", "Invalid email");
              
            }
            return View(homeVM);
        }

   
        public IActionResult Register()
        {
            var response = new HomeUserCreateViewModel();
            return View(response);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}