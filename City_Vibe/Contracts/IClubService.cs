using City_Vibe.Domain.Models;
using City_Vibe.Services.Base;
using City_Vibe.ViewModels.ClubController;


namespace City_Vibe.Contracts
{
    public interface IClubService
    {

        Task<IndexClubViewModel> Index(int category, int page, int pageSize);

        CreateClubViewModel CreateClubGet();

        Task<Response> CreateClubPost(CreateClubViewModel clubVM);

        Task<DetailClubViewModel> DetailClub(int id);

        EditClubViewModel EditClubGet(int id);

        Task<Response> EditClubPost(EditClubViewModel clubVM);

        Task<DeleteClubViewModel> DeleteGet(int id);

        Task<Response> DeleteClubPost(int id);

        Task<Response> AddInInterested(int id);

        ICollection<SaveClub> InterestingСlubsForTheUser();

        Task<Response> AddLikeToTheClub(int clubId);

        PostInformationClubViewModel AddInformationInClubGet(int clubId);

        Task<Response> AddInformationInClubPost(PostInformationClubViewModel postInfo);

        Task<PostInformationDetailViewModel> PostInformationDetail(int postInfoId);
    }
}
