using AutoMapper;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.ExtensionMethod;
using City_Vibe.Services.ResponseModels.ClubResponse;
using City_Vibe.ViewModels.ClubController;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace City_Vibe.Services
{
    public class ClubService : IClubService
    {
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;
        private readonly IHttpContextAccessor сontextAccessor;
        private readonly IUnitOfWork unitOfWorkRepository;
        private readonly IMemoryCache memoryCash;
      


        public ClubService(
            IPhotoService photoServ,
            IHttpContextAccessor сontextAccess,
            IUnitOfWork unitOfWorkRepo,
            IMapper mapp,
            IMemoryCache _memoryCash)
        {
            photoService = photoServ;
            сontextAccessor = сontextAccess;
            unitOfWorkRepository = unitOfWorkRepo;
            mapper = mapp;
            memoryCash = _memoryCash;
        }

        public async Task<IndexClubViewModel> Index(int category, int page, int pageSize)
        {
            string cacheKey = $"clubsCacheKey{category}";

            if (memoryCash.TryGetValue(cacheKey, out IndexClubViewModel clubViewModel))
            {
                if(clubViewModel != null)
                return clubViewModel;
            }

            else
            {
                var byCategory = await unitOfWorkRepository.CategoryRepository.GetByIdAsync(category);


                var clubs = category switch
                {
                    -1 => await unitOfWorkRepository.ClubRepository.GetSliceAsync(page, pageSize, pageSize),
                    _ => await unitOfWorkRepository.ClubRepository.GetClubsByCategoryAndSliceAsync(byCategory, page, pageSize),
                };

                var count = category switch
                {
                    -1 => await unitOfWorkRepository.ClubRepository.GetCountAsync(),
                    _ => await unitOfWorkRepository.ClubRepository.GetCountByCategoryAsync(byCategory),
                };

                List<Category> categories = await unitOfWorkRepository.CategoryRepository.GetAllAsync();

                clubViewModel = new IndexClubViewModel
                {
                    Clubs = clubs,
                    Page = page,
                    PageSize = pageSize,
                    TotalClubs = count,
                    TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                    Category = new SelectList(categories, "Id", "Name", category)
                };


                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                memoryCash.Set(cacheKey, clubViewModel, cacheEntryOptions);
            }

            return clubViewModel;
        }



        public CreateClubViewModel CreateClubGet()
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { AppUserId = curUserId };
            return createClubViewModel;

        }

        public async Task<ClubResponse> CreateClubPost(CreateClubViewModel clubVM)
        {
            ClubResponse response = new ClubResponse();

            var addImage = await photoService.AddPhotoAsync(clubVM.Image);

            if (addImage.Error != null)
            {
                response.PhotoSucceeded = false;
                return response;
            }

            var club = mapper.Map<Club>(clubVM);

            club.Image = addImage.Url.ToString();
            var result = unitOfWorkRepository.ClubRepository.Add(club);
            response.Succeeded = result;

            return response;
        }

        public async Task<DetailClubViewModel> DetailClub(int? id)
        {
            var detailClubViewModel = new DetailClubViewModel();
            var club = await unitOfWorkRepository.ClubRepository.Find(i => i.Id == id).Include(i => i.Address).FirstOrDefaultAsync();

            if (club == null)
            {
                detailClubViewModel.Succeeded = false;
                return detailClubViewModel;
            }


            var events = await unitOfWorkRepository.EventRepository.Find(c => c.ClubId == id).Include(x => x.Address).Include(x => x.Category).ToListAsync();
            var curSaveClub = unitOfWorkRepository.SaveClubRepository.Find(c => c.ClubId == id);
            var countlikes = unitOfWorkRepository.LikeClubRepository.Find(x => x.ClubId == id).ToList().Count();
            var getClubInformation = await unitOfWorkRepository.ClubRepository.GetPostInfoInClubByClubId(id.Value);

            detailClubViewModel = mapper.Map<DetailClubViewModel>(club);
            detailClubViewModel.CountLikes = countlikes;
            detailClubViewModel.Events = events.ToList();

            if (getClubInformation != null)
            {
                detailClubViewModel.PostInfoInClubs = getClubInformation.ToList();
            }

            if (curSaveClub != null)
            {
                detailClubViewModel.SaveClubs = curSaveClub.ToList();
            }
            return detailClubViewModel;
        }

        public EditClubViewModel EditClubGet(int id)
        {
            var clubVM = new EditClubViewModel();
            var club = unitOfWorkRepository.ClubRepository.Find(x => x.Id == id).Include(x => x.Address).FirstOrDefault();
            if (club == null)
            {
                clubVM.Succeeded = false;
                return clubVM;
            }
             clubVM = mapper.Map<EditClubViewModel>(club);
             return clubVM;
        }

        public async Task<ClubResponse> EditClubPost(EditClubViewModel clubVM)
        {
            ClubResponse response = new ClubResponse();
            var userClub = await unitOfWorkRepository.ClubRepository.GetByIdAsyncNoTracking(clubVM.Id);
            if (userClub != null)
            {
                ImageUploadResult addNewPhoto = new ImageUploadResult();

                if (clubVM.Image != null && userClub.Image != null)
                {
                    try
                    {
                        await photoService.DeletePhotoAsync(userClub.Image);
                    }
                    catch (Exception ex)
                    {
                        response.PhotoSucceeded = false;
                        return response;
                    }

                }

                var club = mapper.Map<Club>(clubVM);

                if (clubVM.Image != null)
                {
                    addNewPhoto = await photoService.AddPhotoAsync(clubVM.Image);
                    club.Image = addNewPhoto.Url.ToString();
                }

                    unitOfWorkRepository.ClubRepository.Update(club);
                    response.Succeeded = true;
                   return response;
                
            }
            response.Succeeded = false;
            return response;
        }

        public async Task<DeleteClubViewModel> DeleteGet(int id)
        {

            var clubDetailsVM = new DeleteClubViewModel();
            var clubDetails = await unitOfWorkRepository.ClubRepository.Find(x => x.Id == id).Include(c => c.Category).Include(a=>a.Address).FirstOrDefaultAsync();
            if (clubDetails == null) return clubDetailsVM;

            clubDetailsVM = mapper.Map<DeleteClubViewModel>(clubDetails);
            return clubDetailsVM;

        }

        public async Task<ClubResponse> DeleteClubPost(int id)
        {

            var response = new ClubResponse();
            var clubDetails = await unitOfWorkRepository.ClubRepository.Find(x => x.Id == id).Include(c => c.Category).Include(a => a.Address).FirstOrDefaultAsync();

            if (clubDetails == null)
            {
                response.Succeeded = false;
                return response;
            }

            if (!string.IsNullOrEmpty(clubDetails.Image))
            {
                _ = photoService.DeletePhotoAsync(clubDetails.Image);
            }

            var result =  unitOfWorkRepository.ClubRepository.Delete(clubDetails);

            response.Succeeded = true;
            return response;
        }

        public async Task<ClubResponse> AddInInterested(int id)
        {
            var response = new ClubResponse();

            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            var deleteInterestingClub = unitOfWorkRepository.SaveClubRepository.FindSafeClubusingUserAndClub(id);
            if (deleteInterestingClub.Count != 0)
            {
                var deleteClub = await unitOfWorkRepository.SaveClubRepository.Find(c => c.ClubId == id).FirstOrDefaultAsync();

                if(deleteClub == null)
                {
                    response.Succeeded = false;
                    return response;
                }    

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

            response.Succeeded = true;
            return response;
        }

        public ICollection<SaveClub> InterestingСlubsForTheUser()
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();
            ICollection<SaveClub> result = unitOfWorkRepository.SaveClubRepository.Find(x => x.AppUserId == curUserId).Include(x => x.Club).ToList();
            return result;
        }



        public async Task<ClubResponse> AddLikeToTheClub(int clubId)
        {

            ClubResponse response = new ClubResponse();

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

                var result = unitOfWorkRepository.LikeClubRepository.Add(addLike);
                response.Succeeded = result;
                return response;
            }
            else
            {
                var delete = await unitOfWorkRepository.LikeClubRepository.Find(x => x.AppUserId == curUserId).FirstOrDefaultAsync();

                if (delete != null)
                {
                    var result = unitOfWorkRepository.LikeClubRepository.Delete(delete);
                    response.Succeeded = result;
                    return response;
                }
                else
                {
                    response.Succeeded = false;
                    return response;
                }
            }
        }

        public PostInformationClubViewModel AddInformationInClubGet(int clubId)
        {
            var curUserId = сontextAccessor.HttpContext.User.GetUserId();

            PostInformationClubViewModel postInfoVM = new PostInformationClubViewModel
            {
                ClubId = clubId,
                AppUserId = curUserId,
            };
            return postInfoVM;
        }

        public async Task<ClubResponse> AddInformationInClubPost(PostInformationClubViewModel postInfo)
        {
            ClubResponse response = new ClubResponse();

            var curUserName = сontextAccessor.HttpContext.User.Identity.Name;

            var addPostInfo = mapper.Map<PostInfoInClub>(postInfo);
            addPostInfo.DateAndTime = DateTime.Now;
            addPostInfo.UserName = curUserName;

            if (postInfo.Image != null)
            {
                var result = await photoService.AddPhotoAsync(postInfo.Image);

                if(result.Error != null)
                {
                    response.PhotoSucceeded = false;
                    return response;
                }

                addPostInfo.Image = result.Url.ToString();
            }

           var resultSave = unitOfWorkRepository.ClubRepository.AddPostInfoInClub(addPostInfo);
           response.Succeeded = resultSave;
           return response;
        }

        public async Task<PostInformationDetailViewModel> PostInformationDetail(int postInfoId)
        {
            var clubInfo = await unitOfWorkRepository.ClubRepository.FindByIdPostInfo(postInfoId);
            var listofComment = unitOfWorkRepository.ClubCommentRepository.GetAllCommentsClubById(postInfoId);

            var viewClubInfo = mapper.Map<PostInformationDetailViewModel>(clubInfo);

            if(listofComment != null)
            viewClubInfo.CommentClub = listofComment;

            return viewClubInfo;
        }
    }
}


