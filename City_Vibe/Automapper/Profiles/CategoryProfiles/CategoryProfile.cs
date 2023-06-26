using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.Categories;

namespace City_Vibe.Automapper.Profiles.CategoryProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddViewModel, Category>();

            CreateMap<CategoryEditViewModel, Category>();
            CreateMap<Category, CategoryEditViewModel>();
        }
    }
}
