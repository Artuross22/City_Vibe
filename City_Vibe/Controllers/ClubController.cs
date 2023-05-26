using City_Vibe.ExtensionMethod;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.ClubController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace City_Vibe.Controllers
{
    public class ClubController : Controller
    {
    
        private readonly IPhotoService photoService;
        private readonly IHttpContextAccessor сontextAccessor;
        private readonly ISaveClubRepository saveClubRepository;
        private readonly IUnitOfWork unitOfWorkRepository;


        public ClubController(
            IPhotoService photoServ,
            IHttpContextAccessor сontextAccess,
            ISaveClubRepository saveClubRepo, 
            IUnitOfWork unitOfWorkRepo)
        {
            photoService = photoServ; 
            сontextAccessor = сontextAccess;
            saveClubRepository = saveClubRepo;
            unitOfWorkRepository = unitOfWorkRepo;
        }


        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            var cat = await unitOfWorkRepository.CategoryRepository.GetByIdAsync(category);
      

            var clubs = category switch
            {
                -1 => await unitOfWorkRepository.ClubRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                _ => await unitOfWorkRepository.ClubRepository.GetClubsByCategoryAndSliceAsync(cat, (page - 1) * pageSize, pageSize),
            };

            var count = category switch
            {
                -1 => await unitOfWorkRepository.ClubRepository.GetCountAsync(),
                _ => await unitOfWorkRepository.ClubRepository.GetCountByCategoryAsync(cat),
            };

            List<Category> categories = await unitOfWorkRepository.CategoryRepository.GetAllAsync();

            var clubViewModel = new IndexClubViewModel
            {
                Clubs = clubs,
                Page = page,
                PageSize = pageSize,
                TotalClubs = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category  = new SelectList(categories, "Id", "Name", category)
            };
                     
            return View(clubViewModel);
        }



        [HttpGet]
        public IActionResult CreateClub()
        {

            var CategoryList = unitOfWorkRepository.CategoryRepository.GetAll();
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

                unitOfWorkRepository.ClubRepository.Add(club);
                unitOfWorkRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
        }

   
        public async Task<IActionResult> DetailClub(int id)
        {
            Club club = await unitOfWorkRepository.ClubRepository.GetByIdIncludeAddressAsync(id);
            var events = await unitOfWorkRepository.ClubRepository.GetClubsByEventId(id);
            var curSaveClub = await unitOfWorkRepository.SaveClubRepository.FindClubsByIdAsync(id);
            var countlikes = unitOfWorkRepository.LikeClubRepository.GetLikeClubsByClubId(id);
            var getClubInformation = await unitOfWorkRepository.ClubRepository.GetPostInfoInClubByClubId(id);

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
                CountLikes = countlikes
            };

            if (getClubInformation != null)
            {
                detailClubViewModel.PostInfoInClubs = getClubInformation.ToList();
            }

            if (curSaveClub != null)
            {
                detailClubViewModel.SaveClubs = curSaveClub.ToList();
            }

            return View(detailClubViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditClub(int id)
        {
            var club = await unitOfWorkRepository.ClubRepository.GetByIdAsync(id);
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

            var CategoryList = unitOfWorkRepository.CategoryRepository.GetAll();
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

            var userClub = await unitOfWorkRepository.ClubRepository.GetByIdAsyncNoTracking(id);

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

                unitOfWorkRepository.ClubRepository.Update(club);
                unitOfWorkRepository.Save();

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
            var clubDetails = await unitOfWorkRepository.ClubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }



        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await unitOfWorkRepository.ClubRepository.GetByIdAsync(id);

            if (clubDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(clubDetails.Image))
            {
                _ = photoService.DeletePhotoAsync(clubDetails.Image);
            }

            unitOfWorkRepository.ClubRepository.Delete(clubDetails);
            unitOfWorkRepository.Save();
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> AddInInterested(int id, SaveClub eventList)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var deleteInterestingClub = saveClubRepository.FindSafeClubusingUserAndClub(id);
            if (deleteInterestingClub.Count != 0)
            {
                var deleteClub = await unitOfWorkRepository.SaveClubRepository.FindClubById(id);
                unitOfWorkRepository.SaveClubRepository.Delete(deleteClub);
            }
            else
            {
                var saveClub = new SaveClub
                {
                    AppUserId = curUserId,
                    ClubId = id
                };
                unitOfWorkRepository.SaveClubRepository.Add(saveClub);
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

            var checkLikes = await unitOfWorkRepository.LikeClubRepository.FindLikeByUserIdAndClubId(curUserId, clubId);

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

                unitOfWorkRepository.LikeClubRepository.Add(addLike);
            }
            else
            {
                var delete = await unitOfWorkRepository.LikeClubRepository.FindLikeClubByUserId(curUserId);
                unitOfWorkRepository.LikeClubRepository.Delete(delete);
            }
            return RedirectToAction("DetailClub", new { id = clubId });
        }

        [HttpGet]
        public async Task<ActionResult> AddInformationInClub(int clubId)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            PostInformationClubViewModel postInfoVM = new PostInformationClubViewModel
            {
                ClubId = clubId,
                AppUserId = curUserId,
            };
            return View(postInfoVM);
        }

        [HttpPost]
        public async Task<ActionResult> AddInformationInClub(PostInformationClubViewModel postInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(postInfo);
            }

            var curUserName = сontextAccessor.HttpContext.User.Identity.Name;

            var addPostInfo = new PostInfoInClub
            {
                ClubId = postInfo.ClubId,
                AppUserId = postInfo.AppUserId,
                PostInformation = postInfo.PostInformation,
                DateAndTime = DateTime.Now,
                UserName = curUserName,
            };

            if (postInfo.Image != null)
            {
                var result = await photoService.AddPhotoAsync(postInfo.Image);
                addPostInfo.Image = result.Url.ToString();
            }
            unitOfWorkRepository.ClubRepository.AddPostInfoInClub(addPostInfo);
            return View();
        }


        public async Task<ActionResult> PostInformationDetail(int postInfoId)
        {
            var clubInfo = await unitOfWorkRepository.ClubRepository.FindByIdPostInfo(postInfoId);
            var listofComment = unitOfWorkRepository.ClubCommentRepository.GetAllCommentClubInfoById(postInfoId);

            var viewClubInfo = new PostInformationDetailViewModel
            {
                Id = postInfoId,
                UserName = clubInfo.UserName,
                DateAndTime = clubInfo.DateAndTime,
                PostInformation = clubInfo.PostInformation,
                CommentClub = listofComment,
                Image = clubInfo.Image,

            };


            return View(viewClubInfo);
        }
    }
}
