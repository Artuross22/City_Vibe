using City_Vibe.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using City_Vibe.Domain.Models;
using City_Vibe.Contracts;
using City_Vibe.ValidationAttribute.BaseFilters;
using City_Vibe.RequestModel.NewFolder.Category;

namespace City_Vibe.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService  categoryService;

        public CategoryController(ICategoryService categoryServ) => categoryService = categoryServ;


        public async Task<IActionResult> ListOfCategories()
        {
            IEnumerable<Category> category = await categoryService.ListOfCategories();
            return View(category);
        }

        [HttpGet]
        [ValidateModelAttribute]
        public async Task<ActionResult> EditCategory(BaseRequestCategoryModel requestCategoryBase)
        {
                CategoryEditViewModel category = await categoryService.EditCategoryGet(requestCategoryBase.Id);
                if(category == null || category.Id == 0) return NotFound();
                return View(category);
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
            var request = categoryService.EditCategoryPost(category);
            return RedirectToAction("ListOfCategories", request);
        }


        [HttpGet]
        public IActionResult AddCategory()
        {       
            return View();
        }

        [HttpPost]
        [ValidateModelStateReturnViewAttribute]
        public IActionResult AddCategory(CategoryAddViewModel categoryAddVM)
        {         
                var response = categoryService.AddCategory(categoryAddVM);     
                return RedirectToAction("ListOfCategories", response.Succeeded);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModelAttribute]
        public async Task<IActionResult> DeleteCategory(BaseRequestCategoryModel requestCategoryBase)
        {
            var categoryDelete = await categoryService.DeleteCategory(requestCategoryBase.Id);
            if(categoryDelete == false)  return NotFound();
            return RedirectToAction(nameof(ListOfCategories));      
        }
    }
}

