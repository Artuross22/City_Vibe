using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace City_Vibe.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public CategoryController(IUnitOfWork UnitOfWorkRepository)
        {
            unitOfWorkRepo = UnitOfWorkRepository;
        }

        public async Task<IActionResult> ListOfCategories()
        {
            IEnumerable<Category> category = await unitOfWorkRepo.CategoryRepository.GetAllAsync();
            return View(category);
        }

        [HttpGet]
        public async Task<ActionResult> EditCategory(int? IdEdit)
        {
            if (IdEdit != null)
            {
                Category category = await unitOfWorkRepo.CategoryRepository.GetByIdAsync(IdEdit);
                if (category != null)
                {
                    var categoryViewModel = new CategoryEditViewModel { Name = category.Name, Id = category.Id};
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
            // var categoryUpdate = categoryRepository.GetById(category.Id);
            var categoryUpdate = new Category { Name = category.Name , Id = category.Id};

            unitOfWorkRepo.CategoryRepository.Update(categoryUpdate);
            unitOfWorkRepo.Save();

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
                var category = new Category  { Name = categoryAddVM.Name};
                unitOfWorkRepo.CategoryRepository.Add(category);
                unitOfWorkRepo.Save();
               
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

            var categoryDelete = await unitOfWorkRepo.CategoryRepository.GetByIdAsync(id);

            if (categoryDelete == null)
                return View("Error");

            unitOfWorkRepo.CategoryRepository.Delete(categoryDelete);
            unitOfWorkRepo.Save();

            return RedirectToAction(nameof(ListOfCategories));
           
        }
    }
}

