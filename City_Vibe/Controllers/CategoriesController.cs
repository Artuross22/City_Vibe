using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace City_Vibe.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }

        public async Task<IActionResult> ListOfCategories()
        {
            
            IEnumerable<Category> category  = await categoryRepository.GetAll();
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int? IdEdit)
        {
            if (IdEdit != null)
            {
                Category category = categoryRepository.GetById(IdEdit);
                if (category != null)
                {
                    var categoryViewModel = new CategoriesEditViewModel { Name = category.Name, Id = category.Id};
                    return View(categoryViewModel);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriesEditViewModel category)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit category ");
                return View(category);
            }
            // var categoryUpdate = categoryRepository.GetById(category.Id);
            var categoryUpdate = new Category { Name = category.Name , Id = category.Id};

            categoryRepository.Update(categoryUpdate);

            return RedirectToAction("ListOfCategories");
        }


        [HttpGet]
        public IActionResult AddCategories()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategories(CategoriesAddViewModel categoryAddVM)
        {

            if (ModelState.IsValid)
            {
                var category = new Category  { Name = categoryAddVM.Name};
                categoryRepository.Add(category);
                return RedirectToAction("TestResult");
            }
            return View(categoryAddVM);
        }


       [HttpPost]
       [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var categoryDelete = categoryRepository.GetById(id);

            if (categoryDelete == null)
                return View("Error");

            var deleteCategory = categoryRepository.Delete(categoryDelete);
            return RedirectToAction(nameof(ListOfCategories));
           
        }
    }
}

