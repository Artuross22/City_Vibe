using City_Vibe.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using AutoMapper;

namespace City_Vibe.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWorkRepository;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork UnitOfWorkRepo, IMapper mapp)
        {
            unitOfWorkRepository = UnitOfWorkRepo;
            mapper = mapp; 
        }

        public async Task<IActionResult> ListOfCategories()
        {
            IEnumerable<Category> category = await unitOfWorkRepository.CategoryRepository.GetAllAsync();
            return View(category);
        }

        [HttpGet]
        public async Task<ActionResult> EditCategory(int? IdEdit)
        {
            if (IdEdit != null)
            {
                Category category = await unitOfWorkRepository.CategoryRepository.GetByIdAsync(IdEdit);

                if (category != null)
                {
                    var categoryViewModel = mapper.Map<CategoryEditViewModel>(category);
                    return View(categoryViewModel);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryEditViewModel category)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit category ");
                return View(category);
            }

            var categoryUpdate = mapper.Map<Category>(category);

            unitOfWorkRepository.CategoryRepository.Update(categoryUpdate);
            unitOfWorkRepository.Save();

            return RedirectToAction("ListOfCategories");
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryAddViewModel categoryAddVM)
        {

            if (ModelState.IsValid)
            {
                var category = mapper.Map<Category>(categoryAddVM);
                unitOfWorkRepository.CategoryRepository.Add(category);
                unitOfWorkRepository.Save();       
                return RedirectToAction("ListOfCategories");

            }
            return View(categoryAddVM);
        }


       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var categoryDelete = await unitOfWorkRepository.CategoryRepository.GetByIdAsync(id);

            if (categoryDelete == null)
                return View("Error");

            unitOfWorkRepository.CategoryRepository.Delete(categoryDelete);
            unitOfWorkRepository.Save();

            return RedirectToAction(nameof(ListOfCategories));
           
        }
    }
}

