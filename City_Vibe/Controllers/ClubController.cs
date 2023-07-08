using City_Vibe.ViewModels.ClubController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;

namespace City_Vibe.Controllers
{
    public class ClubController : Controller
    {

        private readonly IUnitOfWork unitOfWorkRepository;
        private readonly IClubService clubService;

        public ClubController(IClubService _clubService, IUnitOfWork _unitOfWorkRepository)
        {
            clubService = _clubService;
            unitOfWorkRepository = _unitOfWorkRepository;
        }

        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }
            var request = await clubService.Index(category, page, pageSize);
            return View(request);

        }

        [HttpGet]
        public IActionResult CreateClub()
        {
            var CategoryList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(CategoryList, "Id", "Name");
            var request = clubService.CreateClubGet();
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClub(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var request = await clubService.CreateClubPost(clubVM);

                if(request.PhotoSucceeded == false)
                {
                    ModelState.AddModelError("", "Photo upload failed");
                }
                return RedirectToAction("Index");
            }
            return View(clubVM);
        }


   
        public async Task<IActionResult> DetailClub(int id)
        {
            var request = await clubService.DetailClub(id);
            if(request.Succeeded == false) return NotFound();
            return View(request);
        }

        [HttpGet]
        public  IActionResult EditClub(int id)
        {
            var request =  clubService.EditClubGet(id);
            if(request.Succeeded == false) return NotFound();

            var CategoryList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(CategoryList, "Id", "Name");

            return View(request);
        }


        [HttpPost]
        public async Task<IActionResult> EditClub(EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("EditClub", clubVM);
            }

            var request = await clubService.EditClubPost(clubVM);
            if(request.PhotoSucceeded == false)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(clubVM);
            }

            if (request.Succeeded == false) return View(clubVM);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await clubService.DeleteGet(id);
            return View(request);
        }



        [HttpPost, ActionName("DeleteGet")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var request = await clubService.DeleteClubPost(id);

            if(request.Success == false) return View(request);

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> AddInInterested(int id)
        {

            var request = await clubService.AddInInterested(id);

            if(request.Success == false) return RedirectToAction(nameof(Index));

            return RedirectToAction("DetailClub", new { id = id });
        }


        public ActionResult InterestingСlubsForTheUser()
        {
            var request = clubService.InterestingСlubsForTheUser();
            return View(request);
        }

        public async Task<ActionResult> AddLikeToTheClub(int clubId)
        {
            var request = await clubService.AddLikeToTheClub(clubId);

            if(request.Success == false) NotFound();

            return RedirectToAction("DetailClub", new { id = clubId });
        }

        [HttpGet]
        public  ActionResult AddInformationInClub(int clubId)
        {
            var request =  clubService.AddInformationInClubGet(clubId);
            return View(request);
        }

        [HttpPost]
        public async Task<ActionResult> AddInformationInClub(PostInformationClubViewModel postInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(postInfo);
            }

            var request = await clubService.AddInformationInClubPost(postInfo);

            if(request.PhotoSucceeded == false)
            {
                ModelState.AddModelError("", "Photo upload failed");
                return View(postInfo);
            }

            return RedirectToAction("DetailClub", new { id = postInfo.ClubId });
        }


        public async Task<ActionResult> PostInformationDetail(int postInfoId)
        {
            var request = await clubService.PostInformationDetail(postInfoId);
            return View(request);
        }
    }
}
