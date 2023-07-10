using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Controllers;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.ExtensionMethod;
using City_Vibe.Infrastructure.Helpers;
using City_Vibe.ViewModels.HomeViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace City_Vibe.Services
{
    public class HomeService : IHomeService
    {

        private readonly IUnitOfWork unitOfWorkRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor сontextAccsess;

        public HomeService(
            IUnitOfWork unitOfWorkRepo,
            UserManager<AppUser> userManagerAccess,
            ILogger<HomeController> _logger,
            IHttpContextAccessor сontextAccs)
        {
            userManager = userManagerAccess;
            сontextAccsess = сontextAccs;
            unitOfWorkRepository = unitOfWorkRepo;
        }

        public async Task<HomeViewModel> IndexGet()
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
                    homeViewModel.Clubs = await unitOfWorkRepository.ClubRepository.GetClubByCity(homeViewModel.City);
                }
                return homeViewModel;
            }
            catch (Exception)
            {
                homeViewModel.Clubs = null;
            }

            return homeViewModel;
        }



        public async Task<HomeViewModel> IndexPost(HomeViewModel homeVM)
        {
            var curUserId = сontextAccsess.HttpContext.User.GetUserId();

            var createVM = homeVM.Register;

            var user = await userManager.FindByEmailAsync(createVM.Email);
            if (user == null)

            {
                homeVM.EmailSucceeded = false;
                return homeVM;
            }

            if (user.Id == curUserId)
            {
                await userManager.AddToRoleAsync(user, UserRoles.ActiveUser);
                homeVM.Succeeded = true;
                return homeVM;
            }
            else
            {
                homeVM.Succeeded = false;
                return homeVM;
            }
        }
    }
}
