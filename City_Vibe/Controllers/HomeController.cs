using City_Vibe.ViewModels.HomeViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using City_Vibe.Domain.Models;
using City_Vibe.Contracts;

namespace City_Vibe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService _homeService) => homeService = _homeService;

        public async Task<IActionResult> Index()
        {
            var request = await homeService.IndexGet();
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel homeVM)
        {

            if (!ModelState.IsValid) return View(homeVM);

            var request = await homeService.IndexPost(homeVM);

            if (request.EmailSucceeded == false)
            {
                ModelState.AddModelError("Register.Email", "This email is not registered");
                return View(homeVM);
            }

            if (request.Succeeded == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Register.Email", "Invalid email");
                return View(homeVM);
            }
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