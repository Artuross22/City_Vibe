using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.ViewModels.ClubController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace City_Vibe.Controllers
{
    public class ClubController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IClubRepository clubRepository;
        private readonly IPhotoService photoService;
        private readonly IHttpContextAccessor сontextAccessor;
        private readonly ISaveClubRepository saveClubRepository;
        private readonly IlikeClubRepository likeClubRepository;

         public readonly ApplicationDbContext dbContext;

        public ClubController(IClubRepository clubRepo, IPhotoService photoServ, IHttpContextAccessor сontextAccess, ICategoryRepository categoryRepo,
            ApplicationDbContext applicationDbContex, ISaveClubRepository saveClubRepo, IlikeClubRepository ilikeClubRepo)
        {
            categoryRepository = categoryRepo;
            clubRepository = clubRepo;
            photoService = photoServ;
            сontextAccessor = сontextAccess;
            dbContext = applicationDbContex;
            saveClubRepository = saveClubRepo;
            likeClubRepository = ilikeClubRepo;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await clubRepository.GetAll();
            return View(clubs);
        }



        [HttpGet]
        public IActionResult CreateClub()
        {

            var CategoryList = categoryRepository.SelectList();
            ViewBag.Categories = new SelectList(CategoryList, "Id", "Name");

            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { AppUserId = curUserId };
            return View(createClubViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClub(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = clubVM.AppUserId,
                    CategoryId = clubVM.CategoryId,
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        Region = clubVM.Address.Region,
                    }
                };

                clubRepository.Add(club);
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
        }

        [HttpGet]
        public async Task<IActionResult> EditClub(int id)
        {
            var club = await clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                Category = club.Category,
                CategoryId = club.CategoryId
            };

            var CategoryList = categoryRepository.SelectList();
            ViewBag.Categories = new SelectList(CategoryList, "Id", "Name");

            return View(clubVM);
        }

        public async Task<IActionResult> DetailClub(int id)
        {
            Club club = await clubRepository.GetByIdAsync(id);
            var events = await clubRepository.GetClubsByEventId(id);
            var curSaveClub = await saveClubRepository.FindClubsByIdAsync(id);

            var countlikes = likeClubRepository.GetLikeClubsByClubId(id);
      

            var detailClubViewModel = new DetailClubViewModel
            {
                Id = id,
                Title = club.Title,
                Description = club.Description,
                Image = club.Image,
                AppUserId = club.AppUserId,
                CategoryId = club.CategoryId,
                Address = new Address
                {
                    Street = club.Address.Street,
                    City = club.Address.City,
                    Region = club.Address.Region,
                },
                Events = events.ToList(),
                SaveClubs = curSaveClub.ToList(),
                CountLikes = countlikes
            };

            return View(detailClubViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditClub(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("EditClub", clubVM);
            }

            var userClub = await clubRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                try
                {
                    await photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(clubVM);
                }
                var photoResult = await photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address,
                    Category = clubVM.Category,
                    CategoryId = clubVM.CategoryId,
                };

                clubRepository.Update(club);

                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }



        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await clubRepository.GetByIdAsync(id);

            if (clubDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(clubDetails.Image))
            {
                _ = photoService.DeletePhotoAsync(clubDetails.Image);
            }

            clubRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> AddInInterested(int id, SaveClub eventList)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var deleteInterestingClub = saveClubRepository.findSafeClubusingUserAndClub(id);
            if (deleteInterestingClub.Count != 0)
            {
                var deleteClub = await saveClubRepository.FindClubById(id);
                saveClubRepository.Delete(deleteClub);
            }
            else
            {
                var saveClub = new SaveClub
                {
                    AppUserId = curUserId,
                    ClubId = id
                };
                saveClubRepository.Add(saveClub);
            }

            return RedirectToAction("DetailClub", new { id = id });
        }


        public async Task<ActionResult> InterestingСlubsForTheUser()
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            ICollection<SaveClub> result = saveClubRepository.FindUserById(curUserId);
            return View(result);
        }

        public async Task<ActionResult> AddLikeToTheClub(int clubId)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var checkLikes = await likeClubRepository.FindLikeByUserIdAndClubId(curUserId, clubId);

            if (checkLikes.Count <= 0)
            {
                int countLike = 0;
                countLike++;

                var addLike = new LikeClub
                {
                    ClubId = clubId,
                    AppUserId = curUserId,
                    Like = countLike,
                };

                likeClubRepository.Add(addLike);
            }
            else
            {
                var delete = await likeClubRepository.FindLikeClubByUserId(curUserId);  // dbContext.LikeClubs.FirstOrDefault(x => x.AppUserId == curUserId);
                likeClubRepository.Delete(delete);
            }
            return RedirectToAction("DetailClub", new { id = clubId });
        }
    }
}
