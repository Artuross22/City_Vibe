using AutoMapper;
using City_Vibe.Domain.Models;
using City_Vibe.ViewModels.Categories;
using Microsoft.Extensions.Logging;

namespace City_Vibe.Automapper.Profiles.CategoryProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddViewModel, Category>()
                   .ForMember(x => x.Id, opt => opt.Ignore())
                   .ForMember(x => x.Events, opt => opt.Ignore());

            CreateMap<CategoryEditViewModel, Category>()
                  .ForMember(x => x.Events, opt => opt.Ignore());

            CreateMap<Category, CategoryEditViewModel>();
        }
    }
}
