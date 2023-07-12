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
            var response = await clubService.Index(category, page, pageSize);
            return View(response);

        }

        [HttpGet]
        public IActionResult CreateClub()
        {
            var CategoryList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(CategoryList, "Id", "Name");
            var response = clubService.CreateClubGet();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClub(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var response = await clubService.CreateClubPost(clubVM);

                if(response.PhotoSucceeded == false)
                {
                    ModelState.AddModelError("", "Photo upload failed");
                    return View(clubVM);
                }
                return RedirectToAction("Index");
            }
            return View(clubVM);
        }


   
        public async Task<IActionResult> DetailClub(int id)
        {
            var response = await clubService.DetailClub(id);
            if(response.Succeeded == false) return NotFound();
            return View(response);
        }

        [HttpGet]
        public  IActionResult EditClub(int id)
        {
            var response =  clubService.EditClubGet(id);
            if(response.Succeeded == false) return NotFound();

            var CategoryList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(CategoryList, "Id", "Name");

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> EditClub(EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                var CategoryList = unitOfWorkRepository.CategoryRepository.GetAll();
                ViewBag.Categories = new SelectList(CategoryList, "Id", "Name");

                return View("EditClub", clubVM);
            }

            var response = await clubService.EditClubPost(clubVM);
            if(response.PhotoSucceeded == false)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(clubVM);
            }

            if (response.Succeeded == false) return View(clubVM);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await clubService.DeleteGet(id);
            return View(response);
        }



        [HttpPost, ActionName("DeleteGet")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var response = await clubService.DeleteClubPost(id);

            if(response.Success == false) return View(response);

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> AddInInterested(int id)
        {

            var response = await clubService.AddInInterested(id);

            if(response.Success == false) return RedirectToAction(nameof(Index));

            return RedirectToAction("DetailClub", new { id = id });
        }


        public ActionResult InterestingСlubsForTheUser()
        {
            var response = clubService.InterestingСlubsForTheUser();
            return View(response);
        }

        public async Task<ActionResult> AddLikeToTheClub(int clubId)
        {
            var response = await clubService.AddLikeToTheClub(clubId);

            if(response.Success == false) NotFound();

            return RedirectToAction("DetailClub", new { id = clubId });
        }

        [HttpGet]
        public  ActionResult AddInformationInClub(int clubId)
        {
            var response =  clubService.AddInformationInClubGet(clubId);
            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddInformationInClub(PostInformationClubViewModel postInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(postInfo);
            }

            var response = await clubService.AddInformationInClubPost(postInfo);

            if(response.PhotoSucceeded == false)
            {
                ModelState.AddModelError("", "Photo upload failed");
                return View(postInfo);
            }

            return RedirectToAction("DetailClub", new { id = postInfo.ClubId });
        }


        public async Task<ActionResult> PostInformationDetail(int postInfoId)
        {
            var response = await clubService.PostInformationDetail(postInfoId);
            return View(response);
        }
    }
}
