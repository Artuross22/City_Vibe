using City_Vibe.ViewModels.DashboardController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Contracts;

namespace City_Vibe.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        public DashboardController(IDashboardService _dashboardService) => dashboardService = _dashboardService;

        public async Task<IActionResult> Index()
        {
            var request = await dashboardService.Index();
            return View(request);
        }

        public async Task<ActionResult> ActiveUsersIndex(int? category, string? name)
        {
            var request = await dashboardService.ActiveUsersIndex(category, name);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> ActiveUsers()
        {
            var request = await dashboardService.ActiveUsers();
            return View(request);
        }
    }
}

