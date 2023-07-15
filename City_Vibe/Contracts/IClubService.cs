using City_Vibe.Domain.Models;
using City_Vibe.Services.Base;
using City_Vibe.Services.ResponseModels.ClubResponse;
using City_Vibe.ViewModels.ClubController;


namespace City_Vibe.Contracts
{
    public interface IClubService
    {

        Task<IndexClubViewModel> Index(int category, int page, int pageSize);

        CreateClubViewModel CreateClubGet();

        Task<ClubResponse> CreateClubPost(CreateClubViewModel clubVM);

        Task<DetailClubViewModel> DetailClub(int id);

        EditClubViewModel EditClubGet(int id);

        Task<ClubResponse> EditClubPost(EditClubViewModel clubVM);

        Task<DeleteClubViewModel> DeleteGet(int id);

        Task<ClubResponse> DeleteClubPost(int id);

        Task<ClubResponse> AddInInterested(int id);

        ICollection<SaveClub> InterestingСlubsForTheUser();

        Task<ClubResponse> AddLikeToTheClub(int clubId);

        PostInformationClubViewModel AddInformationInClubGet(int clubId);

        Task<ClubResponse> AddInformationInClubPost(PostInformationClubViewModel postInfo);

        Task<PostInformationDetailViewModel> PostInformationDetail(int postInfoId);
    }
}
