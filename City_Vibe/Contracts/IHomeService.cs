using City_Vibe.ViewModels.HomeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Contracts
{
    public interface IHomeService
    {

        Task<HomeViewModel> IndexGet();

        Task<HomeViewModel> IndexPost(HomeViewModel homeVM);
    }
}
