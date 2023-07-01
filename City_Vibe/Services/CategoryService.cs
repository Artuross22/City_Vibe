using AutoMapper;
using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Domain.Models;
using City_Vibe.Services.Base;
using City_Vibe.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
namespace City_Vibe.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWorkRepository;
        private readonly IMapper mapper;

        public CategoryService(IMapper mapp, IUnitOfWork unitOfWorkRepo)
        {
            mapper = mapp;
            unitOfWorkRepository = unitOfWorkRepo;
        }

        public Response<int> AddCategory(CategoryAddViewModel categoryAddVM)
        {
            var response = new Response<int>();
            var category = mapper.Map<Category>(categoryAddVM);
            var save = unitOfWorkRepository.CategoryRepository.Add(category);
            if (save == true)
            {
                response.Success = true;
            }
            return response;
        }

        public async Task<bool> DeleteCategory(int? id)
        {
            var categoryDelete = await unitOfWorkRepository.CategoryRepository.GetByIdAsync(id);

            if (categoryDelete == null)
            {
               return categoryDelete == null;
            }
              
           var response =unitOfWorkRepository.CategoryRepository.Delete(categoryDelete);
           return response;
        }

        public async Task<CategoryEditViewModel> EditCategoryGet(int? IdEdit)
        {
            var categoryViewModel = new CategoryEditViewModel();

            Category category = await unitOfWorkRepository.CategoryRepository.GetByIdAsync(IdEdit);
            if (category == null)
            {
                return categoryViewModel;
            }

            categoryViewModel = mapper.Map<CategoryEditViewModel>(category);
            return categoryViewModel;

        }
        public bool EditCategoryPost(CategoryEditViewModel category)
        {

            var categoryUpdate = mapper.Map<Category>(category);
            var response =  unitOfWorkRepository.CategoryRepository.Update(categoryUpdate);
            return response;
        }

        public async Task<IEnumerable<Category>> ListOfCategories()
        {
            IEnumerable<Category> category = await unitOfWorkRepository.CategoryRepository.GetAllAsync();
            return category;
        }
    }
}
