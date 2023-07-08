using City_Vibe.ViewModels.DashboardController;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Contracts
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> Index();

        Task<UserListViewModel> ActiveUsersIndex(int? category, string? name);

        Task<ActiveActionsDasboardVM> ActiveUsers();
    }
}
