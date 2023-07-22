using City_Vibe.ViewModels.ClubController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.ValidationAttribute.BaseFilters;
using System.ComponentModel.DataAnnotations;
using City_Vibe.RequestModel.Club;

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
            var CategoryList = unitOfWorkRepository.CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(CategoryList, "Id", "Name");

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

        [ValidateModelAttribute]
        public async Task<IActionResult> DetailClub(BaseRequestClubModel baseRequestClubModel)
        {        
            var response = await clubService.DetailClub(baseRequestClubModel.Id.Value);
            if(response.Succeeded == false) return NotFound();
            return View(response);
        }

        [HttpGet]
        [ValidateModelAttribute]
        public  IActionResult EditClub(BaseRequestClubModel baseRequestClubModel)
        {
            var response =  clubService.EditClubGet(baseRequestClubModel.Id.Value);
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
        [ValidateModelAttribute]
        public async Task<IActionResult> Delete(BaseRequestClubModel baseRequestClubModel)
        {
            var response = await clubService.DeleteGet(baseRequestClubModel.Id.Value);
            return View(response);
        }



        [HttpPost, ActionName("DeleteGet")]
        [ValidateModelAttribute]
        public async Task<IActionResult> DeleteClub(BaseRequestClubModel baseRequestClubModel)
        {
            var response = await clubService.DeleteClubPost(baseRequestClubModel.Id.Value);

            if(response.Succeeded == false) return View(response);

            return RedirectToAction("Index");
        }

        [ValidateModelAttribute]
        public async Task<ActionResult> AddInInterested(BaseRequestClubModel baseRequestClubModel)
        {
            var response = await clubService.AddInInterested(baseRequestClubModel.Id.Value);

            if(response.Succeeded == false) return RedirectToAction(nameof(Index));

            return RedirectToAction("DetailClub", new { Id = baseRequestClubModel.Id });
        }


        public ActionResult InterestingСlubsForTheUser()
        {
            var response = clubService.InterestingСlubsForTheUser();
            return View(response);
        }

        public async Task<ActionResult> AddLikeToTheClub(BaseRequestClubModel baseRequestClubModel)
        {
            var response = await clubService.AddLikeToTheClub(baseRequestClubModel.Id.Value);

            if(response.Succeeded == false) NotFound();

            return RedirectToAction("DetailClub", new { id = baseRequestClubModel.Id });
        }

        [HttpGet]
        [ValidateModelAttribute]
        public  ActionResult AddInformationInClub([Required] int? clubId)
        {
            var response =  clubService.AddInformationInClubGet(clubId.Value);
            return View(response);
        }

        [HttpPost]
        [ValidateModelStateReturnViewAttribute]
        public async Task<ActionResult> AddInformationInClub(PostInformationClubViewModel postInfo)
        {
            var response = await clubService.AddInformationInClubPost(postInfo);
            if(response.PhotoSucceeded == false)
            {
                ModelState.AddModelError("", "Photo upload failed");
                return View(postInfo);
            }
            return RedirectToAction("DetailClub", new { id = postInfo.ClubId });
        }

        [ValidateModelAttribute]
        public async Task<ActionResult> PostInformationDetail([Required] int? postInfoId)
        {
            var response = await clubService.PostInformationDetail(postInfoId.Value);
            return View(response);
        }
    }
}
