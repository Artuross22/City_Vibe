using City_Vibe.Domain.Models;
using City_Vibe.Services.Base;
using City_Vibe.ViewModels.Categories;

namespace City_Vibe.Contracts
{
    public interface ICategoryService
    {

        Task<IEnumerable<Category>> ListOfCategories();

        Task<CategoryEditViewModel> EditCategoryGet(int? IdEdit);

        bool EditCategoryPost(CategoryEditViewModel category);

        Response AddCategory(CategoryAddViewModel categoryAddVM);

        Task<bool> DeleteCategory(int? id);

    }
}
