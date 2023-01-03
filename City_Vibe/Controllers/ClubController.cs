using City_Vibe.Data;
using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.ViewModels.ClubController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace City_Vibe.Controllers
{
    public class ClubController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IClubRepository clubRepository;
        private readonly IPhotoService photoService;
        public readonly IHttpContextAccessor сontextAccessor;
        public ClubController(IClubRepository clubRepo, IPhotoService photoServ, IHttpContextAccessor сontextAccess, ICategoryRepository categoryRepo)
        {
            categoryRepository = categoryRepo;
            clubRepository = clubRepo;
            photoService = photoServ;
            сontextAccessor = сontextAccess;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> DetailClub(int id)
        {
            Club club = await clubRepository.GetByIdAsync(id);
            return View(club);
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
    }
}
